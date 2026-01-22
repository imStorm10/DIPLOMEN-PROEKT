using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using PCConfigurator.Models;

namespace PCConfigurator.Controllers
{
    public class ConfiguratorController : Controller
    {
        private readonly ExcelPriceService _excelService;

        public ConfiguratorController(ExcelPriceService excelService)
        {
            _excelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate(decimal budget, string type, string cpuBrand, string gpuBrand, string formFactor,
                                      string storageType, string storage2Type,
                                      string ramType, string ramCapacity,
                                      string psuModularity, string psuWattage, string psuRating, string coolerType)
        {
            // 1. Взимане на данните
            var allParts = _excelService.GetAllParts();
            if (allParts.Count == 0) return Json(new { Status = "Error", Message = "Excel файлът е празен!" });

            var cpus = allParts.Where(p => p.Type == "CPU").ToList();
            var gpus = allParts.Where(p => p.Type == "GPU").ToList();
            var mobs = allParts.Where(p => p.Type == "Motherboard").ToList();
            var rams = allParts.Where(p => p.Type == "RAM").ToList();
            var psus = allParts.Where(p => p.Type == "PSU").ToList();
            var cases = allParts.Where(p => p.Type == "Case").ToList();
            var allCoolers = allParts.Where(p => p.Type == "CoolerAir" || p.Type == "CoolerLiquid").ToList();
            var allStorages = allParts.Where(p => p.Type.Contains("Storage") || p.Type.Contains("SSD") || p.Type.Contains("HDD") || p.Type.Contains("Disk")).ToList();

            // ЗАЩИТИ
            if (!cpus.Any() || !gpus.Any() || !mobs.Any()) return Json(new { Status = "Error", Message = "Липсват основни компоненти!" });

            // ==============================================================================
            // 1. ФИЛТРИРАНЕ НА CPU
            // ==============================================================================
            if (budget > 1000)
            {
                cpus = cpus.Where(c => c.ProductName.Contains("-12") || c.ProductName.Contains("-13") || c.ProductName.Contains("-14") || c.ProductName.Contains("7600") || c.ProductName.Contains("7700") || c.ProductName.Contains("7800") || c.ProductName.Contains("7900") || c.ProductName.Contains("9000")).ToList();
            }

            // RAM Type Compatibility
            if (ramType == "DDR5")
            {
                cpus = cpus.Where(c => (!c.ProductName.Contains("-10") && !c.ProductName.Contains("-11")) && (!c.ProductName.Contains("Ryzen 5") && !c.ProductName.Contains("3600") && !c.ProductName.Contains("5600"))).ToList();
            }
            else if (ramType == "DDR4")
            {
                cpus = cpus.Where(c => !c.ProductName.Contains("7600") && !c.ProductName.Contains("7700") && !c.ProductName.Contains("7800") && !c.ProductName.Contains("7900") && !c.ProductName.Contains("9000")).ToList();
            }

            if (cpuBrand != "Any") cpus = cpus.Where(c => c.ProductName.Contains(cpuBrand, StringComparison.OrdinalIgnoreCase)).ToList();

            // ==============================================================================
            // 2. ИЗЧИСЛЕНИЕ НА БЮДЖЕТ И ДИСКОВЕ
            // ==============================================================================
            decimal currentCost = 0;
            currentCost += (psuWattage == "Extreme" || psuWattage == "1000+") ? 150 : 80;

            // 2.1. ИЗБОР НА ПЪРВИ ДИСК
            var selectedStorage = allStorages.Where(s => storageType == "Any" || s.ProductName.Contains(storageType, StringComparison.OrdinalIgnoreCase)).OrderBy(p => p.Price).FirstOrDefault();
            if (selectedStorage != null) currentCost += selectedStorage.Price;

            // 2.2. ИЗБОР НА ВТОРИ ДИСК
            PriceResult selectedStorage2 = null;
            bool needsDualM2 = false; // Флаг за 2 M.2 слота

            if (!string.IsNullOrEmpty(storage2Type) && storage2Type != "None")
            {
                var candidates = allStorages.Where(s => s.ProductName.Contains(storage2Type, StringComparison.OrdinalIgnoreCase)).ToList();
                if (storage2Type == "HDD") candidates = candidates.OrderBy(p => p.Price).ToList();
                else candidates = candidates.OrderBy(p => p.Price).ToList();

                selectedStorage2 = candidates.FirstOrDefault();
                if (selectedStorage2 != null)
                {
                    currentCost += selectedStorage2.Price;

                    // Ако и двата са NVMe (или първият е Any/NVMe и вторият е NVMe)
                    bool firstIsNvme = (storageType == "NVMe" || storageType == "Any");
                    bool secondIsNvme = (storage2Type == "NVMe");

                    if (firstIsNvme && secondIsNvme)
                    {
                        needsDualM2 = true;
                    }
                }
            }

            currentCost += 60 + 50; // Case + Cooler

            decimal remainingBudget = budget - currentCost;
            if (remainingBudget < 200) remainingBudget = 200;

            decimal cpuPct = (type == "Workstation") ? 0.35m : 0.22m;
            decimal gpuPct = (type == "Workstation") ? 0.30m : 0.45m;
            decimal mbPct = 0.20m;
            decimal ramLimit = 90;

            decimal cpuLimit = remainingBudget * cpuPct;
            decimal gpuLimit = remainingBudget * gpuPct;
            decimal mbLimit = remainingBudget * mbPct;
            decimal bonus = 20.0m;

            // 3. ИЗБОР НА ЧАСТИ
            var selectedCPU = cpus.Where(p => p.Price <= cpuLimit + bonus).OrderByDescending(p => p.Price).FirstOrDefault() ?? cpus.OrderBy(p => p.Price).FirstOrDefault();
            var selectedGPU = gpus.Where(p => p.Price <= gpuLimit + bonus).OrderByDescending(p => p.Price).FirstOrDefault() ?? gpus.OrderBy(p => p.Price).FirstOrDefault();

            // RAM Selection
            if (ramCapacity != "Any")
            {
                if (ramCapacity == "8GB") rams = rams.Where(r => r.ProductName.Contains("8GB") || r.ProductName.Contains("2x4")).ToList();
                else if (ramCapacity == "16GB") rams = rams.Where(r => r.ProductName.Contains("16GB") || r.ProductName.Contains("2x8")).ToList();
                else if (ramCapacity == "32GB") rams = rams.Where(r => r.ProductName.Contains("32GB") || r.ProductName.Contains("2x16")).ToList();
                else if (ramCapacity == "64GB") rams = rams.Where(r => r.ProductName.Contains("64GB") || r.ProductName.Contains("2x32")).ToList();
            }
            if (ramType != "Any") rams = rams.Where(r => r.ProductName.Contains(ramType, StringComparison.OrdinalIgnoreCase)).ToList();
            var selectedRAM = rams.Where(p => p.Price <= ramLimit).OrderByDescending(p => p.Price).FirstOrDefault() ?? rams.OrderBy(p => p.Price).FirstOrDefault();

            // 4. ИЗБОР НА ДЪНО (СЪВМЕСТИМОСТ)
            List<PriceResult> compatibleMobs = mobs;
            if (selectedCPU != null)
            {
                bool isIntel = selectedCPU.ProductName.Contains("Intel", StringComparison.OrdinalIgnoreCase);
                if (isIntel)
                {
                    if (selectedCPU.ProductName.Contains("-12") || selectedCPU.ProductName.Contains("-13") || selectedCPU.ProductName.Contains("-14")) compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("1700") || m.ProductName.Contains("690") || m.ProductName.Contains("790") || m.ProductName.Contains("660") || m.ProductName.Contains("760")).ToList();
                    else compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("1200") || m.ProductName.Contains("490") || m.ProductName.Contains("590")).ToList();
                }
                else // AMD
                {
                    if (selectedCPU.ProductName.Contains("7000") || selectedCPU.ProductName.Contains("8000") || selectedCPU.ProductName.Contains("9000")) compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("AM5") || m.ProductName.Contains("650") || m.ProductName.Contains("670")).ToList();
                    else compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("AM4") || m.ProductName.Contains("450") || m.ProductName.Contains("550")).ToList();
                }
            }
            if (selectedRAM != null)
            {
                if (selectedRAM.ProductName.Contains("DDR5")) compatibleMobs = compatibleMobs.Where(m => !m.ProductName.Contains("DDR4") && !m.ProductName.Contains("D4")).ToList();
                else compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("DDR4") || m.ProductName.Contains("D4")).ToList();
            }

            // --- ТУК Е ПРОВЕРКАТА ЗА 2 NVMe ДИСКА ---
            if (needsDualM2)
            {
                // Оставяме само дъна, които имат записано 2 или повече в колоната M2Slots
                compatibleMobs = compatibleMobs.Where(m => m.M2Slots >= 2).ToList();
            }

            if (formFactor == "Micro-ATX") compatibleMobs = compatibleMobs.Where(m => m.ProductName.Contains("Micro") || m.ProductName.Contains("mATX")).ToList();
            else if (formFactor == "ATX") compatibleMobs = compatibleMobs.Where(m => !m.ProductName.Contains("Micro") && !m.ProductName.Contains("mATX")).ToList();

            if (!compatibleMobs.Any()) compatibleMobs = mobs;
            var selectedMB = compatibleMobs.Where(p => p.Price <= mbLimit).OrderByDescending(p => p.Price).FirstOrDefault() ?? compatibleMobs.OrderBy(p => p.Price).FirstOrDefault();

            // 5. ИЗБОР НА КУТИЯ
            bool isMbAtx = !selectedMB.ProductName.Contains("Micro", StringComparison.OrdinalIgnoreCase) && !selectedMB.ProductName.Contains("mATX", StringComparison.OrdinalIgnoreCase);
            var compatibleCases = cases;
            if (isMbAtx) compatibleCases = cases.Where(c => !c.ProductName.Contains("Micro", StringComparison.OrdinalIgnoreCase) && !c.ProductName.Contains("mATX", StringComparison.OrdinalIgnoreCase)).ToList();

            int gpuLen = selectedGPU?.Length ?? 300;
            compatibleCases = compatibleCases.Where(c => (c.Length - 50) >= gpuLen).ToList();
            if (!compatibleCases.Any()) compatibleCases = cases;
            var selectedCase = compatibleCases.OrderBy(p => p.Price).FirstOrDefault();

            // 6. ИЗБОР НА ОХЛАЖДАНЕ
            PriceResult selectedCooler = null;
            string targetSocket = (selectedCPU.ProductName.Contains("Intel")) ?
                ((selectedCPU.ProductName.Contains("-12") || selectedCPU.ProductName.Contains("-13") || selectedCPU.ProductName.Contains("-14")) ? "1700" : "1200") :
                ((selectedCPU.ProductName.Contains("7000") || selectedCPU.ProductName.Contains("9000")) ? "AM5" : "AM4");

            List<PriceResult> candidateCoolers = allCoolers;
            if (coolerType == "Air") candidateCoolers = allCoolers.Where(p => p.Type == "CoolerAir").ToList();
            else if (coolerType == "Liquid") candidateCoolers = allCoolers.Where(p => p.Type == "CoolerLiquid").ToList();

            if (!string.IsNullOrEmpty(targetSocket))
            {
                var compatible = candidateCoolers.Where(c => c.ProductName.Contains(targetSocket, StringComparison.OrdinalIgnoreCase) || c.ProductName.Contains("Universal", StringComparison.OrdinalIgnoreCase)).ToList();
                if (compatible.Any()) candidateCoolers = compatible;
            }

            decimal maxCoolerPrice = budget * 0.12m;
            var affordableCoolers = candidateCoolers.Where(c => c.Price <= maxCoolerPrice).ToList();
            if (!affordableCoolers.Any()) affordableCoolers = candidateCoolers;

            bool highPerf = selectedCPU.Price > 300 || selectedCPU.Wattage > 125;
            if (highPerf) selectedCooler = affordableCoolers.OrderByDescending(c => c.Price).FirstOrDefault();
            else selectedCooler = affordableCoolers.Where(c => c.Price >= 25).OrderBy(c => c.Price).FirstOrDefault() ?? affordableCoolers.OrderBy(c => c.Price).FirstOrDefault();


            // 7. ИЗБОР НА ЗАХРАНВАНЕ
            PriceResult selectedPSU = null;
            if (selectedPSU == null)
            {
                int cpuWatts = selectedCPU?.Wattage ?? 65;
                int gpuWatts = selectedGPU?.Wattage ?? 200;
                int coolerRealConsumption = (selectedCooler?.Type == "CoolerLiquid") ? 15 : 5;
                int systemOverhead = 50;
                if (selectedStorage2 != null) systemOverhead += 15;

                int totalSystemRealDraw = cpuWatts + gpuWatts + coolerRealConsumption + systemOverhead;
                int minRequiredWattage = (int)(totalSystemRealDraw * 1.3); // 30% запас
                if (minRequiredWattage < 500) minRequiredWattage = 500;

                int maxOptimalWattage = minRequiredWattage + 250;

                var safePSUs = psus.Where(p => p.Wattage >= minRequiredWattage).ToList();

                if (psuModularity != "Any")
                {
                    if (psuModularity == "Modular") safePSUs = safePSUs.Where(p => (p.ProductName.Contains("Modular") || p.ProductName.Contains("Full")) && !p.ProductName.Contains("Semi") && !p.ProductName.Contains("Non-Modular") && !p.ProductName.Contains("Non Modular")).ToList();
                    else if (psuModularity == "Semi") safePSUs = safePSUs.Where(p => p.ProductName.Contains("Semi")).ToList();
                    else if (psuModularity == "Non") safePSUs = safePSUs.Where(p => p.ProductName.Contains("Non-Modular") || p.ProductName.Contains("Non Modular") || (!p.ProductName.Contains("Modular") && !p.ProductName.Contains("Semi"))).ToList();
                }

                if (psuWattage == "Medium") safePSUs = safePSUs.Where(p => p.Wattage >= 650 && p.Wattage <= 750).ToList();
                else if (psuWattage == "High") safePSUs = safePSUs.Where(p => p.Wattage >= 850 && p.Wattage < 1000).ToList();

                if (psuRating != "Any") safePSUs = safePSUs.Where(p => p.ProductName.Contains(psuRating, StringComparison.OrdinalIgnoreCase)).ToList();

                var optimalPSUs = safePSUs.Where(p => p.Wattage <= maxOptimalWattage).ToList();
                if (optimalPSUs.Any()) selectedPSU = optimalPSUs.OrderBy(p => p.Price).FirstOrDefault();
                else selectedPSU = safePSUs.OrderBy(p => p.Price).FirstOrDefault();

                if (selectedPSU == null) selectedPSU = psus.OrderByDescending(p => p.Wattage).FirstOrDefault();
            }

            // DOWNGRADE LOOP
            decimal total = CalculateTotal(selectedCPU, selectedGPU, selectedMB, selectedRAM, selectedStorage, selectedPSU, selectedCase, selectedCooler) + (selectedStorage2?.Price ?? 0);
            decimal maxAllowed = budget + 35;

            while (total > maxAllowed)
            {
                var cheaperGPU = gpus.Where(g => g.Price < selectedGPU.Price && g.Price > (selectedGPU.Price - 150)).OrderByDescending(p => p.Price).FirstOrDefault();
                if (cheaperGPU != null)
                {
                    selectedGPU = cheaperGPU;
                    total = CalculateTotal(selectedCPU, selectedGPU, selectedMB, selectedRAM, selectedStorage, selectedPSU, selectedCase, selectedCooler) + (selectedStorage2?.Price ?? 0);
                    if (total <= maxAllowed) break;
                }
                else break;
            }

            int coolerRealWatts = (selectedCooler?.Type == "CoolerLiquid") ? 15 : 5;
            int storage2Watts = (selectedStorage2 != null) ? 10 : 0;
            int displayWattage = (selectedCPU?.Wattage ?? 0) + (selectedGPU?.Wattage ?? 0) + 50 + coolerRealWatts + storage2Watts;

            return Json(new
            {
                Status = "Success",
                TotalPrice = total,
                SystemPower = displayWattage,
                CPU = MapPart(selectedCPU),
                GPU = MapPart(selectedGPU),
                Motherboard = MapPart(selectedMB),
                RAM = MapPart(selectedRAM),
                Storage = MapPart(selectedStorage),
                Storage2 = MapPart(selectedStorage2),
                PSU = MapPart(selectedPSU),
                Case = MapPart(selectedCase),
                Cooler = MapPart(selectedCooler)
            });
        }

        private decimal CalculateTotal(PriceResult cpu, PriceResult gpu, PriceResult mb, PriceResult ram, PriceResult storage, PriceResult psu, PriceResult pcase, PriceResult cooler)
        {
            return (cpu?.Price ?? 0) + (gpu?.Price ?? 0) + (mb?.Price ?? 0) + (ram?.Price ?? 0) + (storage?.Price ?? 0) + (psu?.Price ?? 0) + (pcase?.Price ?? 0) + (cooler?.Price ?? 0);
        }

        private object MapPart(PriceResult part)
        {
            if (part == null) return null;
            string displayFormFactor = null;
            string extraInfo = "";

            if (part.Type == "Motherboard" || part.Type == "Case")
            {
                bool isMicro = part.ProductName.Contains("mATX", StringComparison.OrdinalIgnoreCase) || part.ProductName.Contains("Micro", StringComparison.OrdinalIgnoreCase);
                displayFormFactor = isMicro ? "mATX" : "ATX";
                if (part.Type == "Case") displayFormFactor = $"Max GPU: {part.Length - 50}mm";
            }

            if (part.Type == "GPU") extraInfo = $"{part.Wattage}W | {part.Length}mm";
            if (part.Type == "CPU") extraInfo = $"TDP: {part.Wattage}W";
            if (part.Type == "PSU") extraInfo = $"Output: {part.Wattage}W";
            if (part.Type.Contains("Cooler")) extraInfo = $"Cooling: {part.Wattage}W TDP";

            string finalSpec = string.IsNullOrEmpty(displayFormFactor) ? extraInfo : displayFormFactor;

            return new
            {
                Name = part.ProductName,
                Price = part.Price,
                ImageUrl = part.ImageUrl,
                Url = part.Link,
                FormFactor = finalSpec,
                Type = part.Type
            };
        }
    }
}
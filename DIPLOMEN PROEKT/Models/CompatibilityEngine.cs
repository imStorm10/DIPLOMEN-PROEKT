using PCConfigurator.Data;
using PCConfigurator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCConfigurator.Models
{
    public class CompatibilityEngine
    {
        private readonly ApplicationDbContext _db;

        public CompatibilityEngine(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<object> PickBestConfiguration(decimal budget, string type, string cpuBrand, string gpuBrand, string formFactor)
        {
            // 1. Изтегляме всички данни от базата
            var cpus = await _db.Processors.ToListAsync();
            var gpus = await _db.GPUs.ToListAsync();
            var mobos = await _db.Motherboards.ToListAsync();
            var rams = await _db.RAMs.ToListAsync();
            var psus = await _db.PSUs.ToListAsync();
            var storages = await _db.Storages.ToListAsync();
            var cases = await _db.Cases.ToListAsync();

            // 2. Логика за бюджета
            decimal fixedCosts = (budget < 800) ? 250 : 400;
            decimal performanceBudget = budget - fixedCosts;
            if (performanceBudget < 200) performanceBudget = budget * 0.5m;

            decimal cpuLimit, gpuLimit;
            if (type == "work") { cpuLimit = performanceBudget * 0.60m; gpuLimit = performanceBudget * 0.40m; }
            else { cpuLimit = performanceBudget * 0.35m; gpuLimit = performanceBudget * 0.65m; }

            // 3. Избор на CPU
            var selectedCPU = cpus.Where(c => c.Brand == cpuBrand && c.Price <= cpuLimit).OrderByDescending(c => c.Price).FirstOrDefault()
                              ?? cpus.Where(c => c.Brand == cpuBrand).OrderBy(c => c.Price).FirstOrDefault();

            // 4. Избор на GPU
            var selectedGPU = gpus.Where(g => g.Brand == gpuBrand && g.Price <= gpuLimit).OrderByDescending(g => g.Price).FirstOrDefault()
                              ?? gpus.Where(g => g.Brand == gpuBrand).OrderBy(g => g.Price).FirstOrDefault();

            // 5. Избор на Дъно
            var selectedMB = mobos.Where(m => m.Socket == selectedCPU.Socket && m.FormFactor == formFactor && m.Price <= selectedCPU.Price).OrderByDescending(m => m.Price).FirstOrDefault()
                             ?? mobos.Where(m => m.Socket == selectedCPU.Socket).OrderBy(m => m.Price).FirstOrDefault();

            // 6. RAM
            var selectedRAM = rams.Where(r => r.Type == selectedMB.MemoryType).OrderByDescending(r => r.Price).FirstOrDefault() ?? rams.Last();

            // 7. PSU
            var selectedPSU = psus.Where(p => p.Wattage >= selectedGPU.RecommendedPSU).OrderBy(p => p.Price).FirstOrDefault()
                              ?? psus.OrderByDescending(p => p.Wattage).Last();

            // 8. Storage & Case
            var selectedSSD = storages.OrderBy(s => s.Price).First();
            if (budget > 1500) selectedSSD = storages.OrderByDescending(s => s.Price).First();
            // Търсим дали стрингът съдържа нашия форм фактор (напр. дали "ATX,mATX" съдържа "ATX")
            var selectedCase = cases.Where(c => c.SupportedFormFactors.Contains(selectedMB.FormFactor)).FirstOrDefault() ?? cases.First();

            // 9. Downgrade Loop (Ако е твърде скъпо)
            List<Component> build = new List<Component> { selectedCPU, selectedGPU, selectedMB, selectedRAM, selectedSSD, selectedPSU, selectedCase };
            decimal total = build.Sum(p => p.Price);

            int attempts = 0;
            while (total > budget + 50 && attempts < 10)
            {
                attempts++;
                var cheaperGpu = gpus.Where(g => g.Brand == gpuBrand && g.Price < selectedGPU.Price).OrderByDescending(g => g.Price).FirstOrDefault();
                if (cheaperGpu != null) { selectedGPU = cheaperGpu; build[1] = selectedGPU; }

                total = build.Sum(p => p.Price);
                if (total <= budget + 50) break;

                var cheaperCpu = cpus.Where(c => c.Brand == cpuBrand && c.Price < selectedCPU.Price).OrderByDescending(c => c.Price).FirstOrDefault();
                if (cheaperCpu != null)
                {
                    selectedCPU = cheaperCpu;
                    build[0] = selectedCPU;
                    var newMobo = mobos.FirstOrDefault(m => m.Socket == selectedCPU.Socket && m.FormFactor == formFactor);
                    if (newMobo != null) { selectedMB = newMobo; build[2] = selectedMB; }
                }
                total = build.Sum(p => p.Price);
            }

            int estimatedWattage = selectedCPU.TDP + (selectedGPU.RecommendedPSU / 2) + 50;
            int load = (int)((double)estimatedWattage / selectedPSU.Wattage * 100);

            return new
            {
                Status = "Success",
                Components = build.Select(p => new
                {
                    p.Name,
                    p.Price,
                    ImageUrl = !string.IsNullOrEmpty(p.ImageUrl) ? p.ImageUrl : "https://via.placeholder.com/150",
                    AmazonUrl = "https://www.amazon.de/s?k=" + Uri.EscapeDataString(p.Name)
                }),
                TotalPrice = Math.Round(total, 2),
                Wattage = estimatedWattage,
                PsuWattage = selectedPSU.Wattage,
                Load = load
            };
        }
    }
}
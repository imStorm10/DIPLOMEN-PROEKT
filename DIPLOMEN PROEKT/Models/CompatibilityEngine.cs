using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;

namespace PCConfigurator.Models
{
    public class CompatibilityEngine
    {
        private readonly ApplicationDbContext _db;

        public CompatibilityEngine(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ConfigurationResult?> PickBestConfiguration(int budget, string type, string cpuBrand, string gpuBrand, string formFactor)
        {
            // --- 1. РАЗПРЕДЕЛЕНИЕ НА БЮДЖЕТА ---
            decimal cpuBudgetPct = type == "Gaming" ? 0.20m : 0.35m;
            decimal gpuBudgetPct = type == "Gaming" ? 0.40m : 0.20m;
            decimal moboBudgetPct = 0.12m;
            decimal ramBudgetPct = 0.10m;
            decimal storageBudgetPct = 0.08m;
            decimal caseBudgetPct = 0.05m;
            decimal psuBudgetPct = 0.05m;

            // Оправяне на разминаването в имената (Mapping)
            string dbFormFactor = formFactor;
            if (formFactor == "Micro-ATX") dbFormFactor = "mATX";

            // --- 2. CPU ---
            var cpuQuery = _db.Processors.AsQueryable();
            if (cpuBrand != "Any") cpuQuery = cpuQuery.Where(c => c.Brand == cpuBrand);

            var bestCpu = await cpuQuery
                .Where(c => c.Price <= budget * cpuBudgetPct)
                .OrderByDescending(c => (double)c.Price)
                .FirstOrDefaultAsync();

            if (bestCpu == null) bestCpu = await cpuQuery.OrderBy(c => (double)c.Price).FirstOrDefaultAsync();
            if (bestCpu == null) return null;

            // --- 3. MOTHERBOARD ---
            var moboQuery = _db.Motherboards.AsQueryable();
            moboQuery = moboQuery.Where(m => m.Socket == bestCpu.Socket);

            if (formFactor != "Any")
            {
                moboQuery = moboQuery.Where(m => m.FormFactor == dbFormFactor);
            }

            var bestMotherboard = await moboQuery
                .Where(m => m.Price <= budget * moboBudgetPct)
                .OrderByDescending(m => (double)m.Price)
                .FirstOrDefaultAsync();

            // Fallback ако не намерим точното дъно
            if (bestMotherboard == null)
            {
                bestMotherboard = await _db.Motherboards
                    .Where(m => m.Socket == bestCpu.Socket)
                    .OrderBy(m => (double)m.Price)
                    .FirstOrDefaultAsync();
            }
            if (bestMotherboard == null) return null;

            // --- 4. GPU ---
            var gpuQuery = _db.GPUs.AsQueryable();
            if (gpuBrand != "Any") gpuQuery = gpuQuery.Where(g => g.Brand == gpuBrand);

            var bestGpu = await gpuQuery
                .Where(g => g.Price <= budget * gpuBudgetPct)
                .OrderByDescending(g => (double)g.Price)
                .FirstOrDefaultAsync();

            if (bestGpu == null) bestGpu = await gpuQuery.OrderBy(g => (double)g.Price).FirstOrDefaultAsync();

            // --- 5. RAM ---
            var bestRam = await _db.RAMs
                .Where(r => r.Type == bestMotherboard.MemoryType && r.Price <= budget * ramBudgetPct)
                .OrderByDescending(r => (double)r.Price).FirstOrDefaultAsync();

            if (bestRam == null) bestRam = await _db.RAMs.Where(r => r.Type == bestMotherboard.MemoryType).OrderBy(r => (double)r.Price).FirstOrDefaultAsync();

            // --- 6. STORAGE ---
            var bestStorage = await _db.Storages
                .Where(s => s.Price <= budget * storageBudgetPct)
                .OrderByDescending(s => s.SizeGB)
                .FirstOrDefaultAsync();

            if (bestStorage == null) bestStorage = await _db.Storages.OrderBy(s => (double)s.Price).FirstOrDefaultAsync();

            // --- 7. CASE (КУТИЯ) - ТУК Е НОВАТА ЛОГИКА ---
            var caseQuery = _db.Cases.AsQueryable();

            // Проверка: Огромна ли е видеокартата?
            // (Карти с 3 вентилатора като 4090, 4080, 7900 обикновено искат ATX кутия)
            bool isHugeGpu = false;
            if (bestGpu != null)
            {
                if (bestGpu.Name.Contains("4090") ||
                    bestGpu.Name.Contains("4080") ||
                    bestGpu.Name.Contains("7900") ||
                    bestGpu.Name.Contains("X3D")) // Примерни критерии за големи карти
                {
                    isHugeGpu = true;
                }
            }

            string targetCaseSize = "ATX"; // По подразбиране

            // АКО дъното е малко (mATX) И картата НЕ е огромна -> Търсим малка кутия
            if (bestMotherboard.FormFactor == "mATX" && !isHugeGpu)
            {
                targetCaseSize = "mATX";
            }
            // АКО дъното е голямо (ATX) ИЛИ картата е огромна -> Търсим голяма кутия
            else
            {
                targetCaseSize = "ATX";
            }

            // Търсим кутия, която в описанието си (FormFactor) отговаря на целевия размер
            // ИЛИ в SupportedFormFactors съдържа размера на дъното.

            var bestCase = await caseQuery
                .Where(c => c.FormFactor == targetCaseSize || c.SupportedFormFactors.Contains(bestMotherboard.FormFactor))
                .Where(c => c.Price <= budget * caseBudgetPct)
                .OrderByDescending(c => (double)c.Price)
                .FirstOrDefaultAsync();

            // Ако не намерим специфична кутия, взимаме резервен вариант (ATX побира всичко)
            if (bestCase == null)
            {
                bestCase = await _db.Cases
                    .Where(c => c.SupportedFormFactors.Contains("ATX")) // ATX кутиите събират и mATX дъна
                    .OrderBy(c => (double)c.Price)
                    .FirstOrDefaultAsync();
            }

            // --- 8. PSU ---
            int watts = 500;
            if (bestGpu != null && bestGpu.RecommendedPSU > 0) watts = bestGpu.RecommendedPSU;
            if (bestCpu.TDP > 120) watts += 100;

            var bestPsu = await _db.PSUs
                .Where(p => p.Wattage >= watts && p.Price <= budget * psuBudgetPct)
                .OrderByDescending(p => (double)p.Price)
                .FirstOrDefaultAsync();

            if (bestPsu == null) bestPsu = await _db.PSUs.Where(p => p.Wattage >= watts).OrderBy(p => (double)p.Price).FirstOrDefaultAsync();

            // --- ТОТАЛ ---
            decimal total = 0;
            if (bestCpu != null) total += bestCpu.Price;
            if (bestGpu != null) total += bestGpu.Price;
            if (bestMotherboard != null) total += bestMotherboard.Price;
            if (bestRam != null) total += bestRam.Price;
            if (bestStorage != null) total += bestStorage.Price;
            if (bestCase != null) total += bestCase.Price;
            if (bestPsu != null) total += bestPsu.Price;

            return new ConfigurationResult
            {
                CPU = bestCpu,
                GPU = bestGpu,
                Motherboard = bestMotherboard,
                RAM = bestRam,
                Storage = bestStorage,
                Case = bestCase,
                PSU = bestPsu,
                TotalPrice = total,
                Status = "Success"
            };
        }
    }
}
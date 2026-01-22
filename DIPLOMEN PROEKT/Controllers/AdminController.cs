using Microsoft.AspNetCore.Mvc;
using PCConfigurator.Data;
using PCConfigurator.Models;
using PCConfigurator.Services;
using System.Threading.Tasks;
using System.Linq;

namespace PCConfigurator.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ArdesScraper _scraper;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
            _scraper = new ArdesScraper();
        }

        // Показва страница с бутони
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScrapeCases()
        {
            // 1. Изтриваме старите кутии (по избор, за да не се дублират)
            var oldCases = _db.Cases.ToList();
            _db.Cases.RemoveRange(oldCases);
            await _db.SaveChangesAsync();

            // 2. Теглим нови (напр. 5 страници, можеш да го направиш 24)
            // ВНИМАНИЕ: Ако сложиш 24, ще чакаш около 30-40 секунди!
            var newCases = await _scraper.ScrapeCases(5);

            // 3. Записваме в базата
            if (newCases.Any())
            {
                await _db.Cases.AddRangeAsync(newCases);
                await _db.SaveChangesAsync();
                return Json(new { count = newCases.Count, message = "Успешно изтеглени кутии!" });
            }

            return Json(new { count = 0, message = "Нещо се обърка, няма намерени продукти." });
        }
    }
}
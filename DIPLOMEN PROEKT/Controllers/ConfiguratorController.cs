using Microsoft.AspNetCore.Mvc;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    public class ConfiguratorController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Generate(int budget, string type, string cpuBrand, string gpuBrand, string formFactor)
        {
            var engine = new CompatibilityEngine();
            // Предаваме всички параметри от UI към логиката
            var result = engine.PickBestConfiguration(budget, type, cpuBrand, gpuBrand, formFactor);

            if (result == null) return Json(new { Status = "Error", Message = "Бюджетът е твърде нисък!" });

            return Json(result);
        }
    }
}
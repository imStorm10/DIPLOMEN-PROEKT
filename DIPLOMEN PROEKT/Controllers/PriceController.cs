using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PCConfigurator.Controllers // <-- Провери дали namespace-а е верен!
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ExcelPriceService _excelService;

        public PriceController(ExcelPriceService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet("check")]
        public IActionResult CheckPrice([FromQuery] string productName)
        {
            // 1. Проверяваме дали файла физически е там, където програмата го търси
            string currentDir = Directory.GetCurrentDirectory();
            string expectedPath = Path.Combine(currentDir, "Hardware prices.xlsx");

            if (!System.IO.File.Exists(expectedPath))
            {
                return NotFound($"ГРЕШКА: Файлът ЛИПСВА!\nПрограмата го търси тук:\n{expectedPath}\n\nА той не е там.");
            }

            // 2. Ако файлът е там, търсим продукта
            var result = _excelService.GetPriceFromExcel(productName);

            if (result.Found)
            {
                return Ok(new
                {
                    store = "Excel Database",
                    model = result.ProductName,
                    price = result.Price,
                    link = result.Link,
                    imageUrl = result.ImageUrl
                });
            }

            return NotFound($"Файлът е намерен, но няма продукт с име '{productName}' в него.");
        }
    }
}
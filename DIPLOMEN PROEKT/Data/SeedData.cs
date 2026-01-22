using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PCConfigurator.Models;
using System;
using System.Linq;
using System.IO;           // Добавено за работа с файлове
using OfficeOpenXml;       // Добавено за работа с Excel (EPPlus)

namespace PCConfigurator.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Проверка дали базата вече има данни
                if (context.Processors.Any())
                {
                    return;   // Вече има данни, не правим нищо
                }

                // 1. Процесори (Остават твърдо зададени, както ги имаше)
                context.Processors.AddRange(
                    new Processor
                    {
                        Name = "AMD Ryzen 5 7600",
                        Brand = "AMD",
                        Price = 450.00m,
                        Socket = "AM5",
                        TDP = 65,
                        ImageUrl = "https://ardes.bg/uploads/original/processor-amd-ryzen-5-7600-333554.jpg",
                        Url = "https://ardes.bg/product/amd-ryzen-5-7600-3-8ghz-32mb-l3-am5-100-100001015box-307374"
                    },
                    new Processor
                    {
                        Name = "Intel Core i5-13400F",
                        Brand = "Intel",
                        Price = 420.00m,
                        Socket = "LGA1700",
                        TDP = 65,
                        ImageUrl = "https://ardes.bg/uploads/original/processor-intel-core-i5-13400f-315847.jpg",
                        Url = "https://ardes.bg/product/intel-core-i5-13400f-2-5ghz-20mb-lga1700-bx8071513400f-273676"
                    }
                );

                // 2. Дънни платки
                context.Motherboards.AddRange(
                    new Motherboard
                    {
                        Name = "ASROCK B650M-HDV/M.2",
                        Brand = "ASROCK",
                        Price = 250.00m,
                        Socket = "AM5",
                        FormFactor = "Micro-ATX",
                        MemoryType = "DDR5",
                        ImageUrl = "https://ardes.bg/uploads/original/motherboard-asrock-b650m-hdv-m-2-364233.jpg",
                        Url = "https://ardes.bg/product/asrock-b650m-hdv-m-2-90-mxbla0-a0uayz-319760"
                    },
                    new Motherboard
                    {
                        Name = "GIGABYTE B760M DS3H",
                        Brand = "GIGABYTE",
                        Price = 240.00m,
                        Socket = "LGA1700",
                        FormFactor = "Micro-ATX",
                        MemoryType = "DDR5",
                        ImageUrl = "https://ardes.bg/uploads/original/motherboard-gigabyte-b760m-ds3h-344445.jpg",
                        Url = "https://ardes.bg/product/gigabyte-b760m-ds3h-b760m-ds3h-315758"
                    }
                );

                // 3. Видео карти
                context.GPUs.AddRange(
                    new GPU
                    {
                        Name = "Palit GeForce RTX 4060 Dual",
                        Brand = "NVIDIA",
                        Price = 650.00m,
                        VRAM = 8,
                        RecommendedPSU = 550,
                        ImageUrl = "https://ardes.bg/uploads/original/videocard-palit-geforce-rtx-4060-dual-395721.jpg",
                        Url = "https://ardes.bg/product/palit-geforce-rtx-4060-dual-8gb-ne64060019p1-1070d-338283"
                    }
                );

                // 4. RAM
                context.RAMs.AddRange(
                    new RAM
                    {
                        Name = "Kingston FURY Beast 16GB DDR5",
                        Brand = "Kingston",
                        Price = 140.00m,
                        Type = "DDR5",
                        SizeGB = 16,
                        ImageUrl = "https://ardes.bg/uploads/original/ram-kingston-fury-beast-16gb-ddr5-364112.jpg",
                        Url = "https://ardes.bg/product/16gb-ddr5-6000mhz-kingston-fury-beast-kf560c40bb-16-258054"
                    }
                );

                // =========================================================
                // 5. STORAGE (ЧЕТЕНЕ ОТ EXCEL ЗА HDD / SSD)
                // =========================================================

                // Намираме пътя до файла components.xlsx в папка Data
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "components.xlsx");

                if (File.Exists(filePath))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        // Търсим Sheet с име "Storage". Ако в Excel се казва "Sheet1", промени го тук!
                        var worksheet = package.Workbook.Worksheets["Storage"];

                        if (worksheet != null && worksheet.Dimension != null)
                        {
                            int rowCount = worksheet.Dimension.Rows;

                            // Започваме от ред 2 (прескачаме заглавията)
                            for (int row = 2; row <= rowCount; row++)
                            {
                                // Ако името е празно, пропускаме реда
                                if (worksheet.Cells[row, 1].Value == null) continue;

                                var storage = new Storage
                                {
                                    // РЕДЪТ НА КОЛОНИТЕ В EXCEL ТРЯБВА ДА Е ТАКЪВ:

                                    Name = worksheet.Cells[row, 1].Value.ToString(),      // Кол 1: Име
                                    Brand = worksheet.Cells[row, 2].Value?.ToString() ?? "", // Кол 2: Марка
                                    Price = decimal.Parse(worksheet.Cells[row, 3].Value.ToString()), // Кол 3: Цена

                                    Type = worksheet.Cells[row, 4].Value.ToString(),      // Кол 4: ТИП (SSD или HDD)

                                    SizeGB = int.Parse(worksheet.Cells[row, 5].Value.ToString()), // Кол 5: GB
                                    ImageUrl = worksheet.Cells[row, 6].Value?.ToString() ?? "",   // Кол 6: Снимка
                                    Url = worksheet.Cells[row, 7].Value?.ToString()               // Кол 7: Линк
                                };

                                context.Storages.Add(storage);
                            }
                        }
                    }
                }
                else
                {
                    // Ако Excel файла липсва, слагаме един тестов запис, за да не гърми
                    context.Storages.Add(new Storage
                    {
                        Name = "Липсва Excel файл",
                        Brand = "Error",
                        Type = "SSD",
                        Price = 0,
                        SizeGB = 0
                    });
                }
                // =========================================================

                // 6. Кутия
                context.Cases.AddRange(
                    new Case
                    {
                        Name = "DeepCool CC560",
                        Brand = "DeepCool",
                        Price = 110.00m,
                        FormFactor = "ATX",
                        SupportedFormFactors = "ATX, Micro-ATX, Mini-ITX",
                        ImageUrl = "https://ardes.bg/uploads/original/case-deepcool-cc560-324312.jpg",
                        Url = "https://ardes.bg/product/deepcool-cc560-r-cc560-bkgaa4-g-1-253327"
                    }
                );

                // 7. Захранване
                context.PSUs.AddRange(
                    new PSU
                    {
                        Name = "DeepCool PK650D 650W",
                        Brand = "DeepCool",
                        Price = 100.00m,
                        Wattage = 650,
                        Rating = "Bronze",
                        ImageUrl = "https://ardes.bg/uploads/original/psu-deepcool-pk650d-650w-342231.jpg",
                        Url = "https://ardes.bg/product/650w-deepcool-pk650d-r-pk650d-fa0b-eu-270830"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
using ExcelDataReader;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System;
using PCConfigurator.Models;

namespace PCConfigurator
{
    public class ExcelPriceService
    {
        private readonly string _fileName = "Hardware prices.xlsx";

        public List<PriceResult> GetAllParts()
        {
            var parts = new List<PriceResult>();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _fileName);

            if (!File.Exists(filePath)) return parts;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        if (result.Tables.Count == 0) return parts;
                        var table = result.Tables[0];

                        for (int i = 1; i < table.Rows.Count; i++)
                        {
                            var row = table.Rows[i];

                            string priceString = row[1]?.ToString().Replace("$", "").Replace("€", "").Trim();
                            decimal.TryParse(priceString, out decimal price);

                            // Четене на допълнителните данни
                            int.TryParse(row[5]?.ToString(), out int wattage); // Колона F
                            int.TryParse(row[6]?.ToString(), out int length);  // Колона G
                            int.TryParse(row[7]?.ToString(), out int m2Slots); // Колона H (НОВО)

                            parts.Add(new PriceResult
                            {
                                ProductName = row[0]?.ToString(),
                                Price = price,
                                Link = row[2]?.ToString(),
                                ImageUrl = row[3]?.ToString(),
                                Type = row[4]?.ToString()?.Trim(),
                                Wattage = wattage,
                                Length = length,
                                M2Slots = m2Slots // Запазваме броя слотове
                            });
                        }
                    }
                }
            }
            catch { }
            return parts;
        }
    }
}
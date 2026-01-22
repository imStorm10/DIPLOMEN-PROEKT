using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using PCConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace PCConfigurator.Services
{
    public class ArdesScraper
    {
        public async Task<List<Case>> ScrapeCases(int pagesToScrape = 3)
        {
            var cases = new List<Case>();
            string baseUrl = "https://ardes.bg/komponenti/kompyutarni-kutii?page=";

            var options = new ChromeOptions();
            // Махаме --headless, за да виждаш и решаваш пъзелите
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

            using (var driver = new ChromeDriver(options))
            {
                // Задаваме по-дълъг timeout за зареждане на страницата
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

                for (int i = 1; i <= pagesToScrape; i++)
                {
                    try
                    {
                        string url = baseUrl + i;
                        driver.Navigate().GoToUrl(url);

                        // === ВАЖНА ПРОМЯНА ===
                        if (i == 1)
                        {
                            // Ако е първа страница, чакаме 60 секунди!
                            // ПРЕЗ ТОВА ВРЕМЕ ТИ ТРЯБВА ДА РЕШИШ CAPTCHA-та РЪЧНО!
                            Thread.Sleep(60000);
                        }
                        else
                        {
                            // За следващите страници чакаме по 5 секунди (вече сме валидирани)
                            Thread.Sleep(5000);
                        }
                        // =====================

                        string pageSource = driver.PageSource;
                        var doc = new HtmlDocument();
                        doc.LoadHtml(pageSource);

                        // Търсене на продуктите
                        var productNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product') and contains(@class, 'item')]");
                        if (productNodes == null) productNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'products-grid')]//div");

                        if (productNodes == null) continue;

                        foreach (var node in productNodes)
                        {
                            try
                            {
                                // Име
                                var titleNode = node.SelectSingleNode(".//div[contains(@class, 'title')]//a");
                                if (titleNode == null) continue;

                                string name = System.Net.WebUtility.HtmlDecode(titleNode.InnerText.Trim());
                                string productUrl = titleNode.GetAttributeValue("href", "");
                                if (!productUrl.StartsWith("http")) productUrl = "https://ardes.bg" + productUrl;

                                // Цена
                                var priceNode = node.SelectSingleNode(".//span[contains(@class, 'price-num')]");
                                if (priceNode == null) continue;

                                string priceText = priceNode.InnerText.Replace("лв.", "").Replace("&nbsp;", "").Replace(" ", "").Trim();
                                if (!decimal.TryParse(priceText, NumberStyles.Any, new CultureInfo("bg-BG"), out decimal priceBgn)) continue;

                                decimal priceEuro = Math.Round(priceBgn / 1.95583M, 2);

                                // Снимка
                                var imgNode = node.SelectSingleNode(".//div[contains(@class, 'image')]//img");
                                string imgUrl = "";
                                if (imgNode != null)
                                {
                                    imgUrl = imgNode.GetAttributeValue("src", "");
                                    if (imgUrl.Contains("base64") || string.IsNullOrEmpty(imgUrl))
                                        imgUrl = imgNode.GetAttributeValue("data-original", "");
                                }

                                // Логика за размер
                                string formFactor = "ATX";
                                string supported = "ATX,mATX";

                                if (name.IndexOf("Micro", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    name.IndexOf("Mini", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    name.IndexOf("mATX", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    formFactor = "mATX";
                                    supported = "mATX,ITX";
                                }

                                cases.Add(new Case
                                {
                                    Name = name,
                                    Price = priceEuro,
                                    Brand = DetermineBrand(name),
                                    FormFactor = formFactor,
                                    SupportedFormFactors = supported,
                                    ImageUrl = imgUrl,
                                    Url = productUrl
                                });
                            }
                            catch { continue; }
                        }
                    }
                    catch { break; }
                }
            }

            return cases;
        }

        private string DetermineBrand(string name)
        {
            if (name.Contains("DeepCool", StringComparison.OrdinalIgnoreCase)) return "DeepCool";
            if (name.Contains("NZXT", StringComparison.OrdinalIgnoreCase)) return "NZXT";
            if (name.Contains("Corsair", StringComparison.OrdinalIgnoreCase)) return "Corsair";
            if (name.Contains("Lian Li", StringComparison.OrdinalIgnoreCase)) return "Lian Li";
            if (name.Contains("Aerocool", StringComparison.OrdinalIgnoreCase)) return "Aerocool";
            if (name.Contains("Cougar", StringComparison.OrdinalIgnoreCase)) return "Cougar";
            if (name.Contains("be quiet", StringComparison.OrdinalIgnoreCase)) return "be quiet!";
            if (name.Contains("Fractal", StringComparison.OrdinalIgnoreCase)) return "Fractal Design";
            if (name.Contains("ASUS", StringComparison.OrdinalIgnoreCase)) return "ASUS";
            return "Other";
        }
    }
}
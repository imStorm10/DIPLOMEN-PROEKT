using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace PCConfigurator.Models
{
    public class PriceService
    {
        // Static HttpClient, за да не отваряме нови връзки за всеки продукт
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<decimal> GetLivePrice(string? url, decimal fallbackPrice)
        {
            // 1. Валидация на URL
            if (string.IsNullOrEmpty(url) || !url.Contains("ardes.bg"))
            {
                return fallbackPrice;
            }

            try
            {
                // 2. Настройка на User-Agent (лъжем сайта, че сме истински браузър)
                // Без това Ardes може да ни блокира заявката.
                if (_httpClient.DefaultRequestHeaders.UserAgent.Count == 0)
                {
                    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                }

                // 3. Теглене на HTML
                var html = await _httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // 4. Търсене на цената (Ardes ползват клас 'price-num')
                var priceNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'price-num')]");

                if (priceNode != null)
                {
                    string rawText = priceNode.InnerText; // Пример: "1 250 лв." или "640 €"
                    rawText = WebUtility.HtmlDecode(rawText); // Оправя &nbsp; и други символи

                    // Проверка каква валута ни е върнал сайта
                    bool isEuro = rawText.Contains("€") || rawText.Contains("EUR");
                    bool isBgn = rawText.Contains("лв") || rawText.Contains("BGN");

                    // Изчистване на текста - оставяме само цифри, точка и запетая
                    string cleanPrice = Regex.Replace(rawText, @"[^\d,.]", "");

                    // Оправяне на десетичния разделител (1,250.00 или 1250,00 -> 1250.00)
                    // Ardes обикновено ползват запетая за десетичен знак, а точки няма (ползват интервали).
                    cleanPrice = cleanPrice.Replace(",", ".");

                    // Махаме останали точки, ако са за хиляди (напр. 1.200.50 -> 1200.50)
                    if (cleanPrice.Count(c => c == '.') > 1)
                    {
                        // Това е по-сложен сценарий, но за Ardes обикновено горното replace върши работа
                        // cleanPrice = cleanPrice.Replace(".", ""); // (само ако сме сигурни)
                    }

                    if (decimal.TryParse(cleanPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultPrice))
                    {
                        if (isEuro)
                        {
                            // СУПЕР! Сайтът ни даде Евро директно.
                            return resultPrice;
                        }
                        else if (isBgn)
                        {
                            // Сайтът ни даде Лева (въпреки че искаме Евро).
                            // Трябва да обърнем, иначе в базата ще запишем 2000 евро вместо 1000.
                            return Math.Round(resultPrice / 1.95583M, 2);
                        }
                        else
                        {
                            // Ако не разпознаем валутата, приемаме числото както е (рисково, но връща нещо)
                            return resultPrice;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // При грешка (няма нет, сайта е паднал и т.н.)
                System.Diagnostics.Debug.WriteLine($"Грешка при scraping на {url}: {ex.Message}");
                return fallbackPrice;
            }

            return fallbackPrice;
        }
    }
}
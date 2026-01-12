using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PCConfigurator.Models
{
    public class PriceService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<decimal> GetLivePrice(string? url, decimal fallbackPrice)
        {
            if (string.IsNullOrEmpty(url)) return fallbackPrice;

            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                var html = await _httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var priceNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'price-num')]");

                if (priceNode != null)
                {
                    string cleanPrice = priceNode.InnerText.Replace(" ", "").Replace("лв.", "");
                    decimal priceBgn = decimal.Parse(cleanPrice);
                    return Math.Round(priceBgn / 1.95583M, 2);
                }
            }
            catch (Exception)
            {
                return fallbackPrice;
            }
            return fallbackPrice;
        }
    }
}
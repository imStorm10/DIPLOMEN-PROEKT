using System;

namespace PCConfigurator.Models
{
    public abstract class Component
    {
        public int Id { get; set; } // КЛЮЧ ЗА БАЗАТА ДАННИ
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string? ArdesUrl { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime LastPriceUpdate { get; set; }
    }
}
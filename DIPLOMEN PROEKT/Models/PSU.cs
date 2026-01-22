using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class PSU
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty; // Seasonic, Corsair...
        public decimal Price { get; set; }
        public int Wattage { get; set; } // Мощност
        public string Rating { get; set; } = string.Empty; // 80 Plus Gold, Bronze...
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
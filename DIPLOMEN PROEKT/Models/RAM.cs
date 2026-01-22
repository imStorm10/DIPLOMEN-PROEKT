using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class RAM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty; // Corsair, Kingston...
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty; // DDR4 / DDR5
        public int SizeGB { get; set; } // Капацитет в GB
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
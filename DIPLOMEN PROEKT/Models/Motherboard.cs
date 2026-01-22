using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class Motherboard
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty; // ASUS, MSI...
        public decimal Price { get; set; }
        public string Socket { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty; // ATX, Micro-ATX
        public string MemoryType { get; set; } = string.Empty; // DDR4 / DDR5
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class Processor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public string Brand { get; set; } = string.Empty; // Intel / AMD
        public string Socket { get; set; } = string.Empty; // LGA1700, AM5
        public int TDP { get; set; } // Консумация във Ватове (важно за захранването)
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
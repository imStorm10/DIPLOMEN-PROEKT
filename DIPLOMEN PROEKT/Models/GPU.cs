using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class GPU
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public string Brand { get; set; } = string.Empty; // NVIDIA / AMD
        public int VRAM { get; set; } // GB
        public int RecommendedPSU { get; set; } // Минимално захранване (W)
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
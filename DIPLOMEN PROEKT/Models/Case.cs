using System.ComponentModel.DataAnnotations;

namespace PCConfigurator.Models
{
    public class Case
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty; // NZXT, Corsair...
        public decimal Price { get; set; }
        public string FormFactor { get; set; } = string.Empty; // ATX (физически размер)
        public string SupportedFormFactors { get; set; } = string.Empty; // Какви дъна събира (напр. "ATX, Micro-ATX")
        public string ImageUrl { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}
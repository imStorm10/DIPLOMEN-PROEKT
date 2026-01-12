namespace PCConfigurator.Models
{
    public class GPU : Component
    {
        public int Length { get; set; }                 // Дължина
        public int VRAM { get; set; }                   // Памет
        public int RecommendedPSU { get; set; }         // Препоръчително захранване
    }
}
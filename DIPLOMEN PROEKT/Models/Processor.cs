namespace PCConfigurator.Models
{
    public class Processor : Component
    {
        public string Socket { get; set; } = string.Empty;
        public string Brand { get; set; }
        public int Cores { get; set; }
        public int TDP { get; set; }
        public bool HasIntegratedGpu { get; set; }
    }
}
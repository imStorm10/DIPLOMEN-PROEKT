namespace PCConfigurator.Models
{
    public class RAM : Component
    {
        public int SizeGB { get; set; }     // 16, 32 GB
        public string Type { get; set; }    // DDR4, DDR5
        public int Frequency { get; set; }  // 3200, 6000 MHz
    }
}
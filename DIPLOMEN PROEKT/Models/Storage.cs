namespace PCConfigurator.Models
{
    public class Storage : Component
    {
        public int SizeGB { get; set; }      // 1000 GB (1TB)
        public string Type { get; set; }     // NVMe SSD, SATA SSD, HDD
        public int ReadSpeed { get; set; }   // 3500 MB/s
    }
}
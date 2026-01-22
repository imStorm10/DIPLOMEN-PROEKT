namespace PCConfigurator.Models
{
    public class ConfigurationResult
    {
        public Processor? CPU { get; set; }
        public GPU? GPU { get; set; }
        public Motherboard? Motherboard { get; set; }
        public RAM? RAM { get; set; }
        public Storage? Storage { get; set; }
        public Case? Case { get; set; }
        public PSU? PSU { get; set; }

        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
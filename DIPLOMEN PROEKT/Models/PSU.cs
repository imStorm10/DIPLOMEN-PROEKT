namespace PCConfigurator.Models
{
    public class PSU : Component
    {
        public int Wattage { get; set; }        // 650W, 850W
        public string Rating { get; set; }      // 80+ Gold, Bronze
        public bool IsModular { get; set; }     // Модулно ли е
    }
}
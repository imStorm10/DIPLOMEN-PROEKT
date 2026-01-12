namespace PCConfigurator.Models
{
    public class Motherboard : Component
    {
        public string Socket { get; set; }
        public string FormFactor { get; set; }          // Размер (ATX, mATX)
        public string MemoryType { get; set; }          // Тип памет (DDR4/5)
        public int MaxRamSlots { get; set; }            // Слотове за RAM
    }
}
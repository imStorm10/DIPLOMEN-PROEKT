public class PriceResult
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Link { get; set; }
    public string ImageUrl { get; set; }
    public string Type { get; set; }
    public int Wattage { get; set; }
    public int Length { get; set; }
    public int M2Slots { get; set; } // <--- НОВОТО!
}
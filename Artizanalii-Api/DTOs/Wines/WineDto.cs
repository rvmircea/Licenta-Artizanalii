namespace Artizanalii_Api.DTOs.Wines;

public record WineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int YearCreated { get; set; }
    public double Abv { get; set; }
    
    public int ProducerId { get; set; }
}
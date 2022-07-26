namespace Artizanalii_Api.DTOs.Wines;

public record WineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int YearCreated { get; set; }
    public double Abv { get; set; }
    
    public int ProducerId { get; set; }
}
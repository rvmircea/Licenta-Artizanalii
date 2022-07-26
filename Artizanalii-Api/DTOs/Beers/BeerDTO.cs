using Artizanalii_Api.Entities.Beers;

namespace Artizanalii_Api.DTOs.Beers;

public record BeerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BeerType BeerType { get; set; }
    public decimal Price { get; set; }
    public double Abv { get; set; }
    public int ProducerId { get; set; }
}
using System.Text.Json.Serialization;
using Artizanalii_Api.Entities.Producers;

namespace Artizanalii_Api.Entities.Beers;

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public BeerType BeerType { get; set; }
    public decimal Price { get; set; }
    public double Abv { get; set; }

    public int ProducerId { get; set; }
    [JsonIgnore]
    public Producer Producer { get; set; }
}
using Artizanalii_Api.DTOs.Beers;

namespace Artizanalii_Api.DTOs.Producers;

public record ProducerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int YearFounded { get; set; }
    public int ProducerAddressId { get; set; }
}
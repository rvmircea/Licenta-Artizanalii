using Artizanalii_Api.DTOs.Beers;

namespace Artizanalii_Api.DTOs.Producers;

public class ProducerDTO
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int YearFounded { get; set; }
    public int ProducerAddressId { get; set; }
}
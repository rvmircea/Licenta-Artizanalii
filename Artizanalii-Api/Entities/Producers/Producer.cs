using System.Text.Json.Serialization;
using Artizanalii_Api.Entities.Beers;
using Artizanalii_Api.Entities.ProducerAddresses;
using Artizanalii_Api.Entities.Products;
using Artizanalii_Api.Entities.Wines;

namespace Artizanalii_Api.Entities.Producers;

public class Producer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int YearFounded { get; set; }
    
    public int ProducerAddressId { get; set; }
    public virtual ProducerAddress ProducerAddress { get; set;}
    
    [JsonIgnore]
    public virtual ICollection<Beer> Beers { get; set; }
    [JsonIgnore]
    public virtual ICollection<Wine> Wines { get; set; }
    
    // public virtual ICollection<Product> Products { get; set; }

}
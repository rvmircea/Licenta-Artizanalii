using Artizanalii_Api.Entities.Producers;

namespace Artizanalii_Api.Entities.ProducerAddresses;

public class ProducerAddress
{
    public int Id { get; set; }
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public int AddressNumber { get; set; }
    public int ZipCode { get; set; }
    public virtual Producer Producer { get; set; }
}
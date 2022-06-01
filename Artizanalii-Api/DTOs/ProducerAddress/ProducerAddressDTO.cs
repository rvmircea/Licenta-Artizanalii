namespace Artizanalii_Api.DTOs.ProducerAddress;

public class ProducerAddressDTO
{
    public int Id { get; set; }
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public int AddressNumber { get; set; }
    public int ZipCode { get; set; }
    
}
using System.ComponentModel.DataAnnotations;

namespace Artizanalii_Api.DTOs.ProducerAddress;

public class ProducerAddressDTO
{
    public int Id { get; set; }
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public int AddressNumber { get; set; }
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "ZipCode must have 6 digits")] 
    public int ZipCode { get; set; }
    
}
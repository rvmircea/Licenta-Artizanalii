using System.ComponentModel.DataAnnotations;

namespace Artizanalii_Api.DTOs.ProducerAddress;

public record ProducerAddressDto
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int AddressNumber { get; set; }
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "ZipCode must have 6 digits")] 
    public int ZipCode { get; set; }
    
}
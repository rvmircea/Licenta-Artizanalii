using System.Text.Json.Serialization;
using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.Products;

namespace Artizanalii_Api.Entities.BasketItems;

public class BasketItem
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    [JsonIgnore]
    public int BasketId { get; set; }
    
}
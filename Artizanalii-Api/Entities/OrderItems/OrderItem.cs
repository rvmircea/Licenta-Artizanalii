using Artizanalii_Api.Entities.Products;
using Newtonsoft.Json;

namespace Artizanalii_Api.Entities.OrderItems;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; }
    [JsonIgnore]
    public int OrderId { get; set; }
}
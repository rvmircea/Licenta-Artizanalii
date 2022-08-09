using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.OrderItems;

namespace Artizanalii_Api.Entities.Orders;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime TimeCreated { get; set; } = DateTime.Now;
    
    public int BasketId { get; set; }
    public List<Basket> Baskets { get; set; }
    public decimal TotalPrice { get; set; }
    
    public List<OrderItem> OrderItems { get; set; }
    
}
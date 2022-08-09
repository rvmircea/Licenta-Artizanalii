using Artizanalii_Api.Entities.BasketItems;

namespace Artizanalii_Api.DTOs;

public class BasketDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<BasketItem> BasketItems { get; set; } = new();
    
    public int TotalItems => BasketItems.Aggregate(0, (acc, total) => acc + total.Quantity);
    public decimal TotalPrice => BasketItems.Aggregate(0m, (acc, total) =>
    {
        if (total.Product != null) return acc + total.Product.Price * total.Quantity;
        return 0m;
    });
}
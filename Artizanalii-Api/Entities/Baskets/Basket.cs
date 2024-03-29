﻿using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Entities.Orders;

namespace Artizanalii_Api.Entities.Baskets;

public class Basket
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
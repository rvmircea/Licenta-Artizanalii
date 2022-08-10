using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.OrderItems;
using Artizanalii_Api.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly ArtizanaliiContext _context;

    public OrderRepository(ArtizanaliiContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersFromUserAsync(string userId)
    {
        var orders = await _context.Orders.Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Product)
            .ToListAsync();
        return orders;
    }
    
    public async Task<Order> CreateOrderAsync(string userId, Basket basket)
    {
        if (userId != basket.UserId)
        {
            return null;
        }

        var orderItems = new List<OrderItem>();
        
        foreach (var item in basket.BasketItems)
        {
            orderItems.Add(new OrderItem
            {
                Product = await _context.Products.FindAsync(item.ProductId),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
            });

            var product = await _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();
            if (product is not null && product.StockQuantity - item.Quantity >= 0)
            {
                product.StockQuantity -= item.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        var newOrder = new Order
        {
            Baskets = new List<Basket>(),
            BasketId = basket.Id,
            UserId = userId,
            TotalPrice = basket.TotalPrice,
            OrderItems = orderItems
        };

        foreach (var orderItem in newOrder.OrderItems)
        {
            orderItem.OrderId = newOrder.Id;
        }
        
        var result = _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync();

        
        return result.Entity;
    }
}
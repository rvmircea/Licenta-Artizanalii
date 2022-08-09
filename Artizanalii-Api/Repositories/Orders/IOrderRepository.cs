using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.Orders;

namespace Artizanalii_Api.Repositories.Orders;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersFromUserAsync(string userId);

    Task<Order> CreateOrderAsync(string userId, Basket basket);
}
using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Entities.Baskets;

namespace Artizanalii_Api.Repositories.Baskets;

public interface IBasketRepository
{
    Task<Basket> GetBasketAsync(string userId);

    Task<bool> AddToBasketAsync(string userId, BasketItem basketItem);
    Task<bool> RemoveFromBasketAsync(int productId);
    Task<bool> RemoveAllFromBasketAsync(string userId);
    Task<bool> RemoveItemsFromBasketAsync(int basketId);
}
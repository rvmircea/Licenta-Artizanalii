using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Entities.Baskets;

namespace Artizanalii_Api.Repositories.Baskets;

public interface IBasketRepository
{
    Task<Basket> GetBasket(string userId);

    Task<bool> AddToBasket(string userId, BasketItem basketItem);
}
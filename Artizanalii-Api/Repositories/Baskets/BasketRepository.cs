using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.BasketItems;
using Artizanalii_Api.Entities.Baskets;
using Artizanalii_Api.Entities.Categories;
using Artizanalii_Api.Entities.ProducerAddresses;
using Artizanalii_Api.Entities.Producers;
using Artizanalii_Api.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Baskets;

public class BasketRepository : IBasketRepository
{
    private readonly ArtizanaliiContext _context;

    public BasketRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    
    public async Task<Basket> GetBasket(string userId)
    {
        var userBasket = await _context.Baskets.Where(b => b.UserId == userId).Include(b => b.BasketItems).ThenInclude(b => b.Product).FirstOrDefaultAsync();
        if (userBasket is null)
        {
           return await CreateNewBasket(userId);
        }
        return userBasket;
    }

    public async Task<bool> AddToBasket(string userId, BasketItem basketItem)
    {
        if (userId != basketItem.UserId)
        {
            return false;
        }
        
        var userBasket = await _context.Baskets.Where(b => b.UserId == userId).Include(b => b.BasketItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        if (userBasket is null)
        {
            _context.Baskets.Add(new Basket
            {
                UserId = userId,
            });
            await _context.SaveChangesAsync();
            userBasket = await _context.Baskets.Where(b => b.UserId == userId).Include(b => b.BasketItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        if (userBasket is not null)
        {
            if (userBasket.BasketItems.Any(it => basketItem.Product != null && it.Product != null && it.Product.Id == basketItem.Product.Id))
            {
                foreach (var item in userBasket.BasketItems)
                {
                    if (item.Product.Id == basketItem.Product.Id)
                    {
                        item.Quantity += basketItem.Quantity;
                    }
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                basketItem.BasketId = userBasket.Id;
                var newBasketItem = new BasketItem
                {
                    BasketId = basketItem.BasketId,
                    Quantity = basketItem.Quantity,
                    Product = await _context.Products.FindAsync(basketItem.ProductId),
                };
                _context.BasketItems.Add(newBasketItem);
                await _context.SaveChangesAsync();
            }
        }
        

        return true;
    }

    public async  Task<Basket> CreateNewBasket(string userId)
    {
        _context.Baskets.Add(new Basket
        {
            UserId = userId,
        });
        await _context.SaveChangesAsync();
        return new Basket() {UserId = userId};
    }
    
}
using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Beer;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Beers;

public class BeerRepository : IBeerRepository
{
    private readonly ArtizanaliiContext _context;

    public BeerRepository(ArtizanaliiContext context)
    {
        _context = context;
    }

    public async Task<Beer> CreateBeerAsync(Beer beer)
    {
        var created = await _context.Beers.AddAsync(beer);
        await _context.SaveChangesAsync();
        return created.Entity;
    }

     
    public async Task<ICollection<Beer>> GetAllBeersAsync()
    {
        return await _context.Beers.ToListAsync();
    }

    public async Task<Beer> GetBeerByIdAsync(int beerId)
    {
        return await _context.Beers.FindAsync(beerId);
    }

    public async Task<Beer> DeleteBeerAsync(int beerId)
    {
        var beerToRemove = await _context.Beers.FindAsync(beerId);
   
        var beerRemoved = _context.Beers.Remove(beerToRemove);
        await _context.SaveChangesAsync();

        return beerRemoved.Entity;
    }

    public async Task<Beer> UpdateBeerAsync(int beerId, Beer beer)
    {
        var bereToUpdate = await _context.Beers.FindAsync(beerId);
            
        if(bereToUpdate is not null)
        {
            bereToUpdate.Name = beer.Name;
            bereToUpdate.Price = beer.Price;
            bereToUpdate.Description = beer.Description;
            bereToUpdate.BeerType = beer.BeerType;
        }

        await _context.SaveChangesAsync();
        return bereToUpdate;
    }
}
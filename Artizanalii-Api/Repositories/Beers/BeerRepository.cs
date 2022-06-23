using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Beers;
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
        return await _context.Beers.Include(beer => beer.Producer).ToListAsync();
    }

    public async Task<Beer> GetBeerByIdAsync(int beerId)
    {
        return await _context.Beers.FindAsync(beerId);
    }

    public async Task<Beer> DeleteBeerAsync(int beerId)
    {
        var beerToRemove = await _context.Beers.FindAsync(beerId);

        if (beerToRemove is null) return null;
        var beerRemoved = _context.Beers.Remove(beerToRemove);
        await _context.SaveChangesAsync();

        return beerRemoved.Entity;

    }

    public async Task<Beer> UpdateBeerAsync(int beerId, Beer beer)
    {
        var beerToUpdate = await _context.Beers.FindAsync(beerId);
            
        if(beerToUpdate is not null)
        {
            beerToUpdate.Name = beer.Name;
            beerToUpdate.Price = beer.Price;
            beerToUpdate.Description = beer.Description;
            beerToUpdate.BeerType = beer.BeerType;
            beerToUpdate.Abv = beer.Abv;
            beerToUpdate.ProducerId = beer.ProducerId;
        }

        await _context.SaveChangesAsync();
        return beerToUpdate;
    }
}
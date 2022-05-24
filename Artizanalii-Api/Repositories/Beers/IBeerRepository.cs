using Artizanalii_Api.Entities.Beer;

namespace Artizanalii_Api.Repositories.Beers;

public interface IBeerRepository
{
    Task<ICollection<Beer>> GetAllBeersAsync();
    Task<Beer> GetBeerByIdAsync(int beerId);
    Task<Beer> CreateBeerAsync(Beer beer);
    Task<Beer> UpdateBeerAsync(int beerId, Beer beer);
    Task<Beer> DeleteBeerAsync(int beerId);
}
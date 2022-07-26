using Artizanalii_Api.Entities.Wines;

namespace Artizanalii_Api.Repositories.Wines;

public interface IWineRepository
{
    Task<ICollection<Wine?>> GetAllWinesAsync();
    Task<Wine?> GetWineByIdAsync(int wineId);
    Task<Wine?> CreateWineAsync(Wine? wine);
    Task<Wine> UpdateWineAsync(int wineId, Wine wine);
    Task<Wine?> DeleteWineAsync(int wineId);
}
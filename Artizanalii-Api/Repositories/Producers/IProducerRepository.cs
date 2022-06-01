using Artizanalii_Api.Entities.Producers;

namespace Artizanalii_Api.Repositories.Producers;

public interface IProducerRepository
{
    Task<ICollection<Producer>> GetAllProducersAsync();
    Task<Producer> GetProducerAsync(int producerId);
    Task<Producer> CreateProducerAsync(Producer producer);
    Task<Producer> UpdateProducerAsync(int producerId, Producer producer);
    Task<Producer> DeleteProducerAsync(int producerId);
}
using Artizanalii_Api.Entities.ProducerAddresses;

namespace Artizanalii_Api.Repositories.ProducerAddresses;

public interface IProducerAddressRepository
{
    Task<ICollection<ProducerAddress>> GetAllProducerAddressAsync();
    Task<ProducerAddress> GetProducerAddressAsync(int producerAddressId);
    Task<ProducerAddress> CreateProducerAddressAsync(ProducerAddress producerAddress);
    Task<ProducerAddress> UpdateProducerAddressAsync(int producerAddressId, ProducerAddress producerAddress);
    Task<ProducerAddress> DeleteProducerAddressAsync(int producerAddressId);
}
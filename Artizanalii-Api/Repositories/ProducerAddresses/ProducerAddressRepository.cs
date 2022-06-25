using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.ProducerAddresses;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.ProducerAddresses;

public class ProducerAddressRepository : IProducerAddressRepository
{
    private readonly ArtizanaliiContext _context;

    public ProducerAddressRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    public async Task<ICollection<ProducerAddress>> GetAllProducerAddressAsync()
    {
        return await _context.ProducerAddresses.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<ProducerAddress> GetProducerAddressAsync(int producerAddressId)
    {
        return await _context.ProducerAddresses.FindAsync(producerAddressId);
    }

    public async Task<ProducerAddress> CreateProducerAddressAsync(ProducerAddress producerAddress)
    {
        var producerAddressCreated = await _context.ProducerAddresses.AddAsync(producerAddress);
        await _context.SaveChangesAsync();
        return producerAddressCreated.Entity;
    }

    public async Task<ProducerAddress> UpdateProducerAddressAsync(int producerAddressId, ProducerAddress producerAddress)
    {
        var producerAddressToUpdate = await _context.ProducerAddresses.FindAsync(producerAddressId);
        if (producerAddressToUpdate is not null)
        {
            producerAddressToUpdate.City = producerAddress.City;
            producerAddressToUpdate.Address = producerAddress.Address;
            producerAddressToUpdate.AddressNumber = producerAddress.AddressNumber;
            producerAddressToUpdate.ZipCode = producerAddress.ZipCode;
        }
        else
        {
            return null;
        }

        await _context.SaveChangesAsync();
        return producerAddressToUpdate;
    }

    public async Task<ProducerAddress> DeleteProducerAddressAsync(int producerAddressId)
    {
        var producerAddressToDelete = await _context.ProducerAddresses.FindAsync(producerAddressId);

        if (producerAddressToDelete is null) return null;
        var producerAddressRemoved = _context.ProducerAddresses.Remove(producerAddressToDelete);
        await _context.SaveChangesAsync();

        return producerAddressRemoved.Entity;

    }

}
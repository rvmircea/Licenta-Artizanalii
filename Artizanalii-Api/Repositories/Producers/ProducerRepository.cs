﻿using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Producers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Artizanalii_Api.Repositories.Producers;

public class ProducerRepository : IProducerRepository
{
    private readonly ArtizanaliiContext _context;

    public ProducerRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Producer>> GetAllProducersAsync()
    {
        return await _context.Producers.OrderBy(p => p.Id)
            .Include(p => p.ProducerAddress)
            .ToListAsync();
    }

    public async Task<Producer?> GetProducerAsync(int producerId)
    {
        var entity =  await _context.Producers.Where(p => p.Id == producerId)
            
            .Include(p => p.ProducerAddress)
            .FirstOrDefaultAsync();
        return entity;
    }

    public async Task<Producer?> CreateProducerAsync(Producer? producer)
    {
        var producerCreated = await _context.Producers.AddAsync(producer);
        await _context.SaveChangesAsync();
        return producerCreated.Entity;
    }

    public async Task<Producer?> UpdateProducerAsync(int producerId, Producer? producer)
    {
        var producerToUpdate = await _context.Producers.FindAsync(producerId);
        if (producerToUpdate is not null)
        {  
            producerToUpdate.Name = producer.Name;
            producerToUpdate.Description = producer.Description;
            producerToUpdate.YearFounded = producer.YearFounded;
            producerToUpdate.ProducerAddress = producerToUpdate.ProducerAddress;
        }

        await _context.SaveChangesAsync();
        return producerToUpdate;
    }

    public async Task<Producer?> DeleteProducerAsync(int producerId)
    {
        var producerToRemove = await _context.Producers.FindAsync(producerId);
        var producerRemoved = _context.Producers.Remove(producerToRemove);
        await _context.SaveChangesAsync();
        return producerRemoved.Entity;
    }
}
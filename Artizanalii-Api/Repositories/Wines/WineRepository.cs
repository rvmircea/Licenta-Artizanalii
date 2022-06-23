﻿using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Wines;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Wines;

public class WineRepository : IWineRepository
{
    private readonly ArtizanaliiContext _context;

    WineRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Wine>> GetAllWinesAsync()
    {
        return await _context.Wines.ToListAsync();
    }

    public async Task<Wine> GetWineByIdAsync(int wineId)
    {
        return await _context.Wines.FindAsync(wineId);
    }

    public async Task<Wine> CreateWineAsync(Wine wine)
    {
        var wineCreated = await _context.Wines.AddAsync(wine);
        await _context.SaveChangesAsync();
        return wineCreated.Entity;
    }

    public async Task<Wine> UpdateWineAsync(int wineId, Wine wine)
    {
        var wineToUpdate = await _context.Wines.FindAsync(wineId);

        if (wineToUpdate is not null)
        {
            wineToUpdate.Name = wine.Name;
            wineToUpdate.Price = wine.Price;
            wineToUpdate.Description = wine.Description;
            wineToUpdate.Abv = wine.Abv;
            wineToUpdate.YearCreated = wine.YearCreated;
        }

        await _context.SaveChangesAsync();

        return wineToUpdate;
    }

    public async Task<Wine> DeleteWineAsync(int wineId)
    {
        var beerToRemove = await _context.Wines.FindAsync(wineId);

        var wineRemoved = _context.Wines.Remove(beerToRemove);

        await _context.SaveChangesAsync();
        return wineRemoved.Entity;
    }
}
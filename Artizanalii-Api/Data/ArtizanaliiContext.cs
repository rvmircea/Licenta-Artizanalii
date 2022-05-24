using Artizanalii_Api.Entities.Beer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Data;

public class ArtizanaliiContext : DbContext
{
    public ArtizanaliiContext(DbContextOptions<ArtizanaliiContext> options) : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>().Property(e => e.Price).HasPrecision(12, 10);
        
        base.OnModelCreating(modelBuilder);
    }

}
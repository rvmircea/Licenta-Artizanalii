using Artizanalii_Api.Entities.Beers;
using Artizanalii_Api.Entities.ProducerAddresses;
using Artizanalii_Api.Entities.Producers;
using Artizanalii_Api.Entities.Wines;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Data;

public class ArtizanaliiContext : DbContext
{
    public ArtizanaliiContext(DbContextOptions<ArtizanaliiContext> options) : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Wine> Wines { get; set; }
    public DbSet<Producer?> Producers { get; set; }
    public DbSet<ProducerAddress?> ProducerAddresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>().Property(b => b.Price).HasPrecision(12, 10);
        modelBuilder.Entity<Wine>().Property(w => w.Price).HasPrecision(12, 10);
        
        

        modelBuilder.Entity<ProducerAddress>()
            .HasOne(p => p.Producer)
            .WithOne(pa => pa.ProducerAddress)
            .HasForeignKey<Producer>(p => p.ProducerAddressId);

        modelBuilder.Entity<Beer>()
            .HasOne(b => b.Producer)
            .WithMany(p => p.Beers)
            .HasForeignKey(b => b.ProducerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Wine>()
            .HasOne(w => w.Producer)
            .WithMany(p => p.Wines)
            .HasForeignKey(w => w.ProducerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }

}
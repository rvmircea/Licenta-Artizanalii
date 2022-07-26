using Artizanalii_Api.Entities.Beers;
using Artizanalii_Api.Entities.Categories;
using Artizanalii_Api.Entities.ProducerAddresses;
using Artizanalii_Api.Entities.Producers;
using Artizanalii_Api.Entities.Products;
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
    public DbSet<Wine?> Wines { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<ProducerAddress> ProducerAddresses { get; set; }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Beer>().Property(b => b.Price).HasPrecision(12, 10);
        modelBuilder.Entity<Wine>().Property(w => w.Price).HasPrecision(12, 10);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(12, 6);
        modelBuilder.Entity<Product>().Property(p => p.MSRP).HasPrecision(12, 6);

        modelBuilder.Entity<Product>()
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Product>()
        //     .HasOne(product => product.Producer)
        //     .WithMany(producer => producer.Products)
        //     .HasForeignKey(product => product.ProducerId)
        //     .OnDelete(DeleteBehavior.Cascade);
        
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
        
        
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Beers"
            },
            new Category
            {
                Id = 2,
                Name = "Wines",
            },
            new Category
            {
                Id = 3,
                Name = "Glasses",
            },
            new Category
            {
                Id = 4,
                Name = "CupHolders"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 5,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 6,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 7,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 8,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 9,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            new Product
            {
                Id = 10,
                CategoryId = 1,
                Name = "Madonna Pale Ale",
                Description = "Bere Madonna Pale Ale",
                ImgUrl = "https://images.unsplash.com/photo-1593375548392-d3f977b8a2f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 5.99m,
                MSRP = 5.99m,
                Abv = 6.5,
            },
            
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Madonna Bruna",
                Description = "Brune Deliciouse",
                ImgUrl = "https://adquest.ro/wp-content/uploads/2020/03/1-17.jpg",
                StockQuantity = 500,
                ProducerId = 1003,
                Price = 6.99m,
                MSRP = 6.99m,
                Abv = 7.2,
            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "Dealu' Negru",
                Description = "Vin rosu",
                ImgUrl = "https://images.unsplash.com/photo-1553703189-2f8ce8a11bcc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=686&q=80",
                StockQuantity = 500,
                ProducerId = 1004,
                Price = 12.99m,
                MSRP = 13.99m,
                Abv = 12.8,
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "Jackob's Creek",
                Description = "Shiraz Cabernet",
                ImgUrl = "https://images.unsplash.com/photo-1606657765076-44154cfec14d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1977&q=80",
                StockQuantity = 5,
                ProducerId = 1004,
                Price = 29.99m,
                MSRP = 33.99m,
                Abv = 12.4,
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }

}
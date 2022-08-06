using Artizanalii_Api.Data;
using Artizanalii_Api.DTOs;
using Artizanalii_Api.Entities.Categories;
using Artizanalii_Api.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly ArtizanaliiContext _context;

    public ProductRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await _context.Products.Include(product => product.Producer).ToListAsync();
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products.Where(product => product.Id == productId)
            .Include(product => product.Category)
            .Include(product => product.Producer)
            .ThenInclude(producer => producer.ProducerAddress).FirstOrDefaultAsync();

        if (product is not null && product.Category is not null)
        {
            var category = new Category {Id = product.CategoryId, Name = product.Category.Name};
            product.Category = category;
        }
        
        if (product is not null) return product;
        return null;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products.Where(p => p.CategoryId == categoryId)
            .Include(product => product.Producer).ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductPage(int page)
    {
        const int productsPerPage = 8;
        
        var products = await _context.Products
            .Include(product => product.Producer)
            .Skip((page - 1) * productsPerPage)
            .Take(productsPerPage).ToListAsync();
        return products;
    }

    public async Task<Product?> GetProductBySearch(string producerName, int id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id && p.Producer.Name.ToLower().Contains(producerName) )
            .Include(p => p.Producer)
            .FirstOrDefaultAsync();
        return product ?? null;
    }
    
}
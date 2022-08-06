using Artizanalii_Api.Entities.Products;

namespace Artizanalii_Api.Repositories.Products;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<List<Product>> GetProductPage(int page);
    Task<Product?> GetProductBySearch(string producerName, int id);

}
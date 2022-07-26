using Artizanalii_Api.Entities.Products;

namespace Artizanalii_Api.Entities.Categories;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}
using Artizanalii_Api.Entities.Products;
using Newtonsoft.Json;

namespace Artizanalii_Api.Entities.Categories;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Product>? Products { get; set; } = new();
}
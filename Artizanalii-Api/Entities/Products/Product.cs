using Artizanalii_Api.Entities.Categories;
using Artizanalii_Api.Entities.Producers;

namespace Artizanalii_Api.Entities.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal MSRP { get; set; }
    public double? Abv { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    
    public int StockQuantity { get; set; }
    
    public Producer? Producer { get; set; }
    public int ProducerId { get; set; }
    
}
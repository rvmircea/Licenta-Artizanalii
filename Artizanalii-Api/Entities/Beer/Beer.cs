namespace Artizanalii_Api.Entities.Beer;

public class Beer
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public string Description { get; set; }
    public BeerType BeerType { get; set; }
    public decimal Price { get; set; }
    
}
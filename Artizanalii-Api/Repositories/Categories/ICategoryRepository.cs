using Artizanalii_Api.Entities.Categories;

namespace Artizanalii_Api.Repositories.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
}
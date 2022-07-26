using Artizanalii_Api.Data;
using Artizanalii_Api.Entities.Categories;
using Microsoft.EntityFrameworkCore;

namespace Artizanalii_Api.Repositories.Categories;

public class CategoryRepository :ICategoryRepository
{
    private readonly ArtizanaliiContext _context;

    public CategoryRepository(ArtizanaliiContext context)
    {
        _context = context;
    }
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }
}
using Lection1007.Contexts;
using Lection1007.Filters;
using Lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1007.Services;

public class CategoryService(StoreDbContext context)
{
    private readonly StoreDbContext _context = context;

    public async Task<List<Category>> GetCategoriesAsync()
        => await _context.Categories.ToListAsync();
}

using ECommerce.Services.Products.Core.Entities;
using ECommerce.Services.Products.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Products.Core.DAL.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly ProductsDbContext _context;

    public CategoryRepository(ProductsDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _context.Categories.AnyAsync(x => x.Name == name);
    }

    public Task<Category> GetAsync(string name)
    {
        return _context.Categories.SingleOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<Category>> BrowseAsync()
    {
        return await _context.Categories.Include(x => x.Products).ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
}
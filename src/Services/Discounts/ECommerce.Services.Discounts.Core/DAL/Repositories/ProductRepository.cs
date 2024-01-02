using ECommerce.Services.Discounts.Core.Entities;
using ECommerce.Services.Discounts.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Discounts.Core.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly DiscountsDbContext _context;

    public ProductRepository(DiscountsDbContext context)
    {
        _context = context;
    }

    public Task<Product> GetAsync(Guid id)
    {
        return _context.Products.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
    {
        return await _context.Products
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
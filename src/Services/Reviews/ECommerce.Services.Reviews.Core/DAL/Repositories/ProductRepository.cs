using ECommerce.Services.Reviews.Core.Entities;
using ECommerce.Services.Reviews.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Reviews.Core.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ReviewsDbContext _context;

    public ProductRepository(ReviewsDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Products.AnyAsync(x => x.Id == id);
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
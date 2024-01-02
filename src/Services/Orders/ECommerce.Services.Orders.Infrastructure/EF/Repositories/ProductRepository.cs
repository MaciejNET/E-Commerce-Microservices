using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly OrdersDbContext _context;

    public ProductRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<Product> GetAsync(AggregateId id)
    {
        return _context.Products.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> BrowsAsync(IEnumerable<Guid> ids)
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

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
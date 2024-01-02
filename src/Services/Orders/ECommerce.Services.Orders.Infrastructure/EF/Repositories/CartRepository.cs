using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CartRepository : ICartRepository
{
    private readonly OrdersDbContext _context;

    public CartRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<Cart> GetAsync(AggregateId id)
    {
        return _context.Carts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<Cart> GetAsync(UserId id)
    {
        return _context.Carts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == id);
    }

    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }
}
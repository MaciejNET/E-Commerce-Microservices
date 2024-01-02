using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CartItemRepository : ICartItemRepository
{
    private readonly OrdersDbContext _context;

    public CartItemRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<CartItem> GetAsync(EntityId id)
    {
        return _context.CartItems.Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<CartItem> items)
    {
        _context.CartItems.UpdateRange(items);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CartItem item)
    {
        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<CartItem> items)
    {
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
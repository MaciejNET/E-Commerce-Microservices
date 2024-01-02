using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CheckoutCartItemRepository : ICheckoutCartItemRepository
{
    private readonly OrdersDbContext _context;

    public CheckoutCartItemRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CheckoutCartItem item)
    {
        await _context.CheckoutCartItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<CheckoutCartItem> items)
    {
        await _context.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<CheckoutCartItem> items)
    {
        _context.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
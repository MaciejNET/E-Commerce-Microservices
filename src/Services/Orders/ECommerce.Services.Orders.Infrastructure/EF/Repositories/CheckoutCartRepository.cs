using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CheckoutCartRepository : ICheckoutCartRepository
{
    private readonly OrdersDbContext _context;

    public CheckoutCartRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<CheckoutCart> GetAsync(AggregateId id)
    {
        return _context.CheckoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Include(x => x.Discount)
            .Include(x => x.Shipment)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<CheckoutCart> GetAsync(UserId userId)
    {
        return _context.CheckoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Include(x => x.Discount)
            .Include(x => x.Shipment)
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task AddAsync(CheckoutCart checkoutCart)
    {
        _context.CheckoutCarts.Entry(checkoutCart).State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CheckoutCart checkoutCart)
    {
        _context.CheckoutCarts.Update(checkoutCart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CheckoutCart checkoutCart)
    {
        _context.CheckoutCarts.Remove(checkoutCart);
        await _context.SaveChangesAsync();
    }
}
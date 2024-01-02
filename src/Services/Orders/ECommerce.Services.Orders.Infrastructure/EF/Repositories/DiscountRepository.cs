using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class DiscountRepository : IDiscountRepository
{
    private readonly OrdersDbContext _context;

    public DiscountRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<Discount> GetAsync(AggregateId id)
    {
        return _context.Discounts.SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<Discount> GetAsync(string code)
    {
        return _context.Discounts.SingleOrDefaultAsync(x => x.Code == code);
    }

    public async Task AddAsync(Discount discount)
    {
        await _context.Discounts.AddAsync(discount);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Discount discount)
    {
        _context.Remove(discount);
        await _context.SaveChangesAsync();
    }
}
using ECommerce.Services.Orders.Domain.Orders.Entities;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly OrdersDbContext _context;

    public OrderRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<Order> GetAsync(AggregateId id)
    {
        return _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Order>> BrowseAsync(UserId id)
    {
        return await _context.Orders
            .Where(x => x.UserId == id)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
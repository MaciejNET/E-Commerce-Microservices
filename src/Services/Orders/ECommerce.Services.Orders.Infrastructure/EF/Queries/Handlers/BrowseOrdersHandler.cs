using Convey.CQRS.Queries;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Services.Orders.Application.Orders.DTO;
using ECommerce.Services.Orders.Application.Orders.Queries;
using ECommerce.Services.Orders.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class BrowseOrdersHandler : IQueryHandler<BrowseOrders, IEnumerable<OrderDto>>
{
    private readonly DbSet<Order> _orders;

    public BrowseOrdersHandler(OrdersDbContext context)
    {
        _orders = context.Orders;
    }

    public async Task<IEnumerable<OrderDto>> HandleAsync(BrowseOrders query, CancellationToken cancellationToken)
    {
        var ordersQuery = _orders.AsQueryable();

        if (query.UserId is not null) ordersQuery = ordersQuery.Where(x => x.UserId == query.UserId);

        if (query.IsCompleted is not null) ordersQuery = ordersQuery.Where(x => x.IsCompleted == query.IsCompleted);

        ordersQuery = ordersQuery.OrderByDescending(x => x.PlaceDate);

        var orders = await ordersQuery.ToListAsync(cancellationToken: cancellationToken);

        return orders.Select(x => x.AsDto());
    }
}
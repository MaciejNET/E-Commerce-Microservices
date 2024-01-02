using Convey.CQRS.Queries;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Services.Orders.Application.Orders.DTO;
using ECommerce.Services.Orders.Application.Orders.Queries;
using ECommerce.Services.Orders.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
{
    private readonly DbSet<Order> _orders;

    public GetOrderHandler(OrdersDbContext context)
    {
        _orders = context.Orders;
    }

    public async Task<OrderDto> HandleAsync(GetOrder query, CancellationToken cancellationToken)
    {
        var order = await _orders
            .Include(x => x.Lines)
            .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        return order.AsDto();
    }
}
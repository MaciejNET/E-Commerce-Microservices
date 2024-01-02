using Convey.CQRS.Queries;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Services.Orders.Application.Carts.DTO;
using ECommerce.Services.Orders.Application.Carts.Queries;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetCartHandler : IQueryHandler<GetCart, CartDto>
{
    private readonly DbSet<Cart> _carts;

    public GetCartHandler(OrdersDbContext context)
    {
        _carts = context.Carts;
    }

    public async Task<CartDto> HandleAsync(GetCart query, CancellationToken cancellationToken)
    {
        var cart = await _carts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == query.UserId, cancellationToken: cancellationToken);

        return cart.AsDto();
    }
}
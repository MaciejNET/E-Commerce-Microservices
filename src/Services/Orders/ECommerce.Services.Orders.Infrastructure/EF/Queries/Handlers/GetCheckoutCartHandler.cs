using Convey.CQRS.Queries;
using ECommerce.Modules.Orders.Infrastructure.EF.Mappings;
using ECommerce.Services.Orders.Application.Carts.DTO;
using ECommerce.Services.Orders.Application.Carts.Queries;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Queries.Handlers;

internal sealed class GetCheckoutCartHandler : IQueryHandler<GetCheckoutCart, CheckoutCartDto>
{
    private readonly DbSet<CheckoutCart> _checkoutCarts;

    public GetCheckoutCartHandler(OrdersDbContext context)
    {
        _checkoutCarts = context.CheckoutCarts;
    }

    public async Task<CheckoutCartDto> HandleAsync(GetCheckoutCart query, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == query.UserId, cancellationToken: cancellationToken);

        return checkoutCart.AsDto();
    }
}
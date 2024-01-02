using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class CancelCheckoutHandler : ICommandHandler<CancelCheckout>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public CancelCheckoutHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(CancelCheckout command, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null) throw new CartNotCheckedOutException(command.UserId);

        await _checkoutCartRepository.DeleteAsync(checkoutCart);
    }
}
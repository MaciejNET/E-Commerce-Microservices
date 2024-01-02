using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class AddPaymentHandler : ICommandHandler<AddPayment>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public AddPaymentHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(AddPayment command, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null) throw new CartNotCheckedOutException(command.UserId);

        checkoutCart.SetPayment(command.PaymentMethod);

        await _checkoutCartRepository.UpdateAsync(checkoutCart);
    }
}
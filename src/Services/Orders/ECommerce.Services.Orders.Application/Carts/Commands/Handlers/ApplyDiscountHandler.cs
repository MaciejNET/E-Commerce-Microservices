using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class ApplyDiscountHandler : ICommandHandler<ApplyDiscount>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IDiscountRepository _discountRepository;

    public ApplyDiscountHandler(ICheckoutCartRepository checkoutCartRepository, IDiscountRepository discountRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
        _discountRepository = discountRepository;
    }

    public async Task HandleAsync(ApplyDiscount command, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null) throw new CartNotCheckedOutException(command.UserId);

        var discount = await _discountRepository.GetAsync(command.Code);

        if (discount is null) throw new DiscountNotFoundException(command.Code);

        checkoutCart.ApplyDiscount(discount);
        await _checkoutCartRepository.UpdateAsync(checkoutCart);
    }
}
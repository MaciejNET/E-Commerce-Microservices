using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Services.Orders.Domain.Carts.Services;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class CheckoutCartHandler : ICommandHandler<CheckoutCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly IExchangeRateService _exchangeRateService;

    public CheckoutCartHandler(ICartRepository cartRepository, ICheckoutCartRepository checkoutCartRepository,
        IDomainEventDispatcher dispatcher, IExchangeRateService exchangeRateService)
    {
        _cartRepository = cartRepository;
        _checkoutCartRepository = checkoutCartRepository;
        _dispatcher = dispatcher;
        _exchangeRateService = exchangeRateService;
    }

    public async Task HandleAsync(CheckoutCart command, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetAsync(new UserId(command.UserId));

        if (cart is null) throw new CartNotFoundException(command.UserId);

        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is not null) throw new CartAlreadyCheckedOutException(command.UserId);

        await cart.Checkout(_exchangeRateService);
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}
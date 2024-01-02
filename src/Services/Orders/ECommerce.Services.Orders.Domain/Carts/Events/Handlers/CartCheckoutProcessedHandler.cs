using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Events.Handlers;

public sealed class CartCheckoutProcessedHandler : IDomainEventHandler<CartCheckoutProcessed>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutCartItemRepository _checkoutCartItemRepository;
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IDomainEventDispatcher _dispatcher;

    public CartCheckoutProcessedHandler(ICartRepository cartRepository, ICheckoutCartRepository checkoutCartRepository,
        IDomainEventDispatcher dispatcher, ICheckoutCartItemRepository checkoutCartItemRepository)
    {
        _cartRepository = cartRepository;
        _checkoutCartRepository = checkoutCartRepository;
        _dispatcher = dispatcher;
        _checkoutCartItemRepository = checkoutCartItemRepository;
    }

    public async Task HandleAsync(CartCheckoutProcessed @event)
    {
        var cart = await _cartRepository.GetAsync(new UserId(@event.UserId));
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(@event.UserId));
        var checkoutCartItems = checkoutCart.Items;

        await _checkoutCartItemRepository.DeleteRangeAsync(checkoutCartItems);
        await _checkoutCartRepository.DeleteAsync(checkoutCart);
        cart.Clear();
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}
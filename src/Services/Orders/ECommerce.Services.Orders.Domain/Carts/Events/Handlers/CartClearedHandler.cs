using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Domain.Carts.Events.Handlers;

public sealed class CartClearedHandler : IDomainEventHandler<CartCleared>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ILogger<CartClearedHandler> _logger;

    public CartClearedHandler(ICartItemRepository cartItemRepository, ILogger<CartClearedHandler> logger)
    {
        _cartItemRepository = cartItemRepository;
        _logger = logger;
    }

    public async Task HandleAsync(CartCleared @event)
    {
        await _cartItemRepository.DeleteRangeAsync(@event.CartItems);
        _logger.LogInformation("Items from cart with ID: {CartId} has been removed", @event.CartId);
    }
}
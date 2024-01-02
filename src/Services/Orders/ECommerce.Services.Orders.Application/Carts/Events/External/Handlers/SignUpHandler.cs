using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Carts.Events.External.Handlers;

internal class SignUpHandler : IEventHandler<SignedUp>
{
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<SignUpHandler> _logger;

    public SignUpHandler(ICartRepository cartRepository, ILogger<SignUpHandler> logger)
    {
        _cartRepository = cartRepository;
        _logger = logger;
    }

    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetAsync(new UserId(@event.UserId));

        if (cart is not null)
        {
            _logger.LogError("Cart for user with ID: '{UserId}' already exists", cart.UserId.ToString());
            return;
        }

        cart = Cart.Create(
            new AggregateId(Guid.NewGuid()),
            @event.UserId,
            @event.PreferredCurrency);

        await _cartRepository.AddAsync(cart);
        _logger.LogInformation("Cart for user with ID: '{UserId}' has been created", @event.UserId);
    }
}
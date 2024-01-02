using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class ClearCartHandler : ICommandHandler<ClearCart>
{
    private readonly IBusPublisher _busPublisher;
    private readonly ICartRepository _cartRepository;
    private readonly IDomainEventDispatcher _dispatcher;

    public ClearCartHandler(ICartRepository cartRepository, IBusPublisher busPublisher,
        IDomainEventDispatcher dispatcher)
    {
        _cartRepository = cartRepository;
        _busPublisher = busPublisher;
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(ClearCart command, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetAsync(new UserId(command.UserId));

        if (cart is null) throw new CartNotFoundException(command.UserId);

        cart.Clear();
        await _cartRepository.UpdateAsync(cart);
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}
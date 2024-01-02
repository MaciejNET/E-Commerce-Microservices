using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using ECommerce.Services.Orders.Application.Carts.Events;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Time;
using OrderPlaced = ECommerce.Services.Orders.Application.Carts.Events.OrderPlaced;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class PlaceOrderHandler : ICommandHandler<PlaceOrder>
{
    private readonly IBusPublisher _busPublisher;
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IClock _clock;
    private readonly IDomainEventDispatcher _dispatcher;

    public PlaceOrderHandler(ICheckoutCartRepository checkoutCartRepository, IClock clock,
        IDomainEventDispatcher dispatcher, IBusPublisher busPublisher)
    {
        _checkoutCartRepository = checkoutCartRepository;
        _clock = clock;
        _dispatcher = dispatcher;
        _busPublisher = busPublisher;
    }

    public async Task HandleAsync(PlaceOrder command, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null) throw new CheckoutCartNotFoundException(command.UserId);

        var orderId = new AggregateId();
        checkoutCart.PlaceOrder(_clock, orderId);
        await _busPublisher.PublishAsync(new OrderPlaced(
            orderId,
            _clock.CurrentDate(),
            checkoutCart.Items.Select(x => x.Product.Sku)));
        await _dispatcher.DispatchAsync(checkoutCart.Events.ToArray());

        var integrationEvents = (IEnumerable<IEvent>) checkoutCart.Events.Select(x => x switch
        {
            ProductBought p => new ProductBought(p.ProductId, p.Quantity),
            _ => null
        });

        await _busPublisher.PublishAsync(integrationEvents.ToArray());
    }
}
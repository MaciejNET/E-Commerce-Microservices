using ECommerce.Services.Orders.Domain.Orders.Entities;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Domain.Carts.Events.Handlers;

public sealed class OrderPlacedHandler : IDomainEventHandler<OrderPlaced>
{
    private readonly ILogger<OrderPlacedHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderPlacedHandler(IOrderRepository orderRepository, ILogger<OrderPlacedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderPlaced @event)
    {
        var order = Order.CreateFromCheckout(@event.CheckoutCart, @event.Now, @event.Id);

        await _orderRepository.AddAsync(order);
        _logger.LogInformation("Created order with ID: '{OrderId}'", order.Id.ToString());
    }
}
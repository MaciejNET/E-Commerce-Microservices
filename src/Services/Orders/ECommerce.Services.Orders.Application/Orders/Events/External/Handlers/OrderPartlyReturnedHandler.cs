using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Orders.Events.External.Handlers;

internal sealed class OrderPartlyReturnedHandler : IEventHandler<OrderPartlyReturned>
{
    private readonly ILogger<OrderPartlyReturnedHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderPartlyReturnedHandler(IOrderRepository orderRepository, ILogger<OrderPartlyReturnedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderPartlyReturned @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.PartlyReturn();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' partly return", order.Id.Value);
    }
}
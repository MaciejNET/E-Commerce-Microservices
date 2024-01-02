using Convey.CQRS.Events;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Orders.Application.Orders.Events.External.Handlers;

internal sealed class OrderReturnedHandler : IEventHandler<OrderReturned>
{
    private readonly ILogger<OrderReturnedHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderReturnedHandler(IOrderRepository orderRepository, ILogger<OrderReturnedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderReturned @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.Return();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' return", order.Id.Value);
    }
}
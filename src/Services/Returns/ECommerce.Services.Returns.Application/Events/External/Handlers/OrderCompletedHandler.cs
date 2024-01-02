using Convey.CQRS.Events;
using ECommerce.Services.Returns.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Application.Events.External.Handlers;

internal sealed class OrderCompletedHandler : IEventHandler<OrderCompleted>
{
    private readonly ILogger<OrderCompletedHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCompletedHandler(IOrderRepository orderRepository, ILogger<OrderCompletedHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderCompleted @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.Complete(@event.Now);
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' has been completed", order.Id.ToString());
    }
}
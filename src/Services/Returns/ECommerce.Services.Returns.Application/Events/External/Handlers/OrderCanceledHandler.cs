using Convey.CQRS.Events;
using ECommerce.Services.Returns.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Application.Events.External.Handlers;

internal sealed class OrderCanceledHandler : IEventHandler<OrderCanceled>
{
    private readonly ILogger<OrderCanceledHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCanceledHandler(IOrderRepository orderRepository, ILogger<OrderCanceledHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderCanceled @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' has been canceled", order.Id.ToString());
    }
}
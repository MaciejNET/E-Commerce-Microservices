using Convey.CQRS.Events;
using ECommerce.Services.Returns.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Application.Events.External.Handlers;

internal sealed class OrderSentHandler : IEventHandler<OrderSent>
{
    private readonly ILogger<OrderSentHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderSentHandler(IOrderRepository orderRepository, ILogger<OrderSentHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderSent @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.Send();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' has been sent", order.Id.ToString());
    }
}
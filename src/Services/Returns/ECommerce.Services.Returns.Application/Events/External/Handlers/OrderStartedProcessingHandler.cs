using Convey.CQRS.Events;
using ECommerce.Services.Returns.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Application.Events.External.Handlers;

internal sealed class OrderStartedProcessingHandler : IEventHandler<OrderStartedProcessing>
{
    private readonly ILogger<OrderStartedProcessingHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderStartedProcessingHandler(IOrderRepository orderRepository,
        ILogger<OrderStartedProcessingHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderStartedProcessing @event, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(@event.Id);

        if (order is null)
        {
            _logger.LogError("Order with ID: '{Id}' does not exists", @event.Id);
            return;
        }

        order.StartProcessing();
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order with ID: '{Id}' started processing", order.Id.ToString());
    }
}
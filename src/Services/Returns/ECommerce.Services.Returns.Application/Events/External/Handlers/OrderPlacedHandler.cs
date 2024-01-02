using Convey.CQRS.Events;
using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Services.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Application.Events.External.Handlers;

internal sealed class OrderPlacedHandler : IEventHandler<OrderPlaced>
{
    private readonly ILogger<OrderPlacedHandler> _logger;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderPlacedHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository,
        ILogger<OrderPlacedHandler> logger)
    {
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _logger = logger;
    }

    public async Task HandleAsync(OrderPlaced @event, CancellationToken cancellationToken)
    {
        var orderProducts = @event.ProductSkus.Select(x => OrderProduct.Create(x, @event.Id)).ToList();
        var order = Order.Create(@event.Id, orderProducts, @event.OrderPlace, OrderStatus.Placed);

        await _orderRepository.AddAsync(order);
        await _orderProductRepository.AddRangeAsync(orderProducts);
        _logger.LogInformation("Order with ID: {Id} has been added", order.Id);
    }
}
using Convey.MessageBrokers;
using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Services.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Returns.Domain.Events.Handlers;

internal sealed class ReturnStatusChangedHandler : IDomainEventHandler<ReturnStatusChanged>
{
    private readonly IBusPublisher _busPublisher;
    private readonly ILogger<ReturnStatusChangedHandler> _logger;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IOrderRepository _orderRepository;

    public ReturnStatusChangedHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository,
        ILogger<ReturnStatusChangedHandler> logger, IBusPublisher busPublisher)
    {
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _logger = logger;
        _busPublisher = busPublisher;
    }

    public async Task HandleAsync(ReturnStatusChanged @event)
    {
        var order = await _orderRepository.GetAsync(@event.OrderId);
        var orderProduct = await _orderProductRepository.GetAsync(@event.OrderProductId);

        switch (@event.Status)
        {
            case ReturnStatus.Accepted:
            {
                orderProduct.Return();
                var isOrderFullyReturn = order.Products.All(x => x.IsReturn);

                if (!isOrderFullyReturn)
                {
                    order.PartlyReturn();
                    await _busPublisher.PublishAsync(new OrderPartlyReturned(order.Id));
                    _logger.LogInformation("Order with ID: '{Id}' partly return", @event.OrderId);
                }
                else
                {
                    order.Return();
                    await _busPublisher.PublishAsync(new OrderReturned(order.Id));
                    _logger.LogInformation("Order with ID: '{Id}' fully return", @event.OrderId);
                }

                break;
            }

            case ReturnStatus.Declined:
                _logger.LogInformation("Return for product: '{Sku}' from order with ID: '{Id}' has been declined",
                    orderProduct.Sku, order.Id.ToString());
                break;

            case ReturnStatus.SendToManualCheck:
                _logger.LogInformation(
                    "Return for product: '{Sku}' from order with ID: '{Id}' has been sent to manual check",
                    orderProduct.Sku, order.Id.ToString());
                break;

            default:
                _logger.LogError("Invalid return status for product: '{Sku}' from order with ID: '{Id}'",
                    orderProduct.Sku, order.Id.ToString());
                break;
        }

        await _orderRepository.UpdateAsync(order);
        await _orderProductRepository.UpdateAsync(orderProduct);
    }
}
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using ECommerce.Services.Orders.Application.Orders.Events;
using ECommerce.Services.Orders.Application.Orders.Exceptions;
using ECommerce.Services.Orders.Domain.Orders.Repositories;

namespace ECommerce.Services.Orders.Application.Orders.Commands.Handlers;

internal sealed class StartProcessingOrderHandler : ICommandHandler<StartProcessingOrder>
{
    private readonly IBusPublisher _busPublisher;
    private readonly IOrderRepository _orderRepository;

    public StartProcessingOrderHandler(IOrderRepository orderRepository, IBusPublisher busPublisher)
    {
        _orderRepository = orderRepository;
        _busPublisher = busPublisher;
    }

    public async Task HandleAsync(StartProcessingOrder command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null) throw new OrderNotFoundException(command.Id);

        order.StartProcessing();
        await _orderRepository.UpdateAsync(order);
        await _busPublisher.PublishAsync(new OrderStartedProcessing(order.Id));
    }
}
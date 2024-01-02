using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using ECommerce.Services.Orders.Application.Orders.Events;
using ECommerce.Services.Orders.Application.Orders.Exceptions;
using ECommerce.Services.Orders.Domain.Orders.Repositories;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Services.Orders.Application.Orders.Commands.Handlers;

internal sealed class CompleteOrderHandler : ICommandHandler<CompleteOrder>
{
    private readonly IBusPublisher _busPublisher;
    private readonly IClock _clock;
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderHandler(IOrderRepository orderRepository, IClock clock, IBusPublisher busPublisher)
    {
        _orderRepository = orderRepository;
        _clock = clock;
        _busPublisher = busPublisher;
    }

    public async Task HandleAsync(CompleteOrder command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(command.Id);

        if (order is null) throw new OrderNotFoundException(command.Id);

        order.Complete(_clock.CurrentDate());
        await _orderRepository.UpdateAsync(order);
        await _busPublisher.PublishAsync(new OrderCompleted(order.Id, order.CompletionDate!.Value));
    }
}
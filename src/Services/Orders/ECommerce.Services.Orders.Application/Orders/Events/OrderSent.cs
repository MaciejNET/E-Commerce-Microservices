using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Orders.Events;

public record OrderSent(Guid Id) : IEvent;
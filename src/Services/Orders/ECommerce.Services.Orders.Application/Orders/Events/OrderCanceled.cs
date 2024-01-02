using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Orders.Events;

public record OrderCanceled(Guid Id) : IEvent;
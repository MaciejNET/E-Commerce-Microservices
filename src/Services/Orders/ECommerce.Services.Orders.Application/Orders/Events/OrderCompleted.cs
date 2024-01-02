using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Orders.Events;

public record OrderCompleted(Guid Id, DateTime Now) : IEvent;
using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Orders.Events.External;

public record OrderPartlyReturned(Guid Id) : IEvent;
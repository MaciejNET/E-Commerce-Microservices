using Convey.CQRS.Events;

namespace ECommerce.Services.Returns.Domain.Events;

public record OrderReturned(Guid Id) : IEvent;
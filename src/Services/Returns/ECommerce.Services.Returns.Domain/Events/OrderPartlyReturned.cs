using Convey.CQRS.Events;

namespace ECommerce.Services.Returns.Domain.Events;

public record OrderPartlyReturned(Guid Id) : IEvent;
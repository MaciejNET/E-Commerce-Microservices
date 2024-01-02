using Convey.CQRS.Events;

namespace ECommerce.Services.Products.Core.Events;

public record ProductDeleted(Guid Id) : IEvent;
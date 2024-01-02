using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("products")]
public record ProductDeleted(Guid Id) : IEvent;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Returns.Application.Events.External;

[Message("orders")]
public record OrderCompleted(Guid Id, DateTime Now) : IEvent;
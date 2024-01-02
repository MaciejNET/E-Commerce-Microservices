using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Returns.Application.Events.External;

[Message("orders")]
public record OrderPlaced(Guid Id, DateTime OrderPlace, IEnumerable<string> ProductSkus) : IEvent;
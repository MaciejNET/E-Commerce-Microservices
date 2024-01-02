using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Discounts.Core.Events.External;

[Message("products")]
internal record ProductDeleted(Guid Id) : IEvent;
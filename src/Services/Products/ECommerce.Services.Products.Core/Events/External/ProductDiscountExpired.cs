using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Products.Core.Events.External;

[Message("discounts")]
public record ProductDiscountExpired(Guid ProductId) : IEvent;
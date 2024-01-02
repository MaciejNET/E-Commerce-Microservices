using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("discounts")]
public record DiscountCodeExpired(Guid Id) : IEvent;
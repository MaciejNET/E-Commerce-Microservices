using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("discounts")]
public record DiscountCodeAdded(Guid Id, string Code, int Percentage, List<Guid> ProductIds) : IEvent;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("discounts")]
public record ProductDiscountAdded(Guid ProductId, Price NewPrice) : IEvent;
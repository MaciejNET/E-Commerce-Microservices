using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Carts.Events;

public record OrderPlaced(Guid Id, DateTime OrderPlace, IEnumerable<string> ProductSkus) : IEvent;
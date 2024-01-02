using Convey.CQRS.Events;

namespace ECommerce.Services.Orders.Application.Carts.Events;

public record ProductBought(Guid ProductId, int Quantity) : IEvent;
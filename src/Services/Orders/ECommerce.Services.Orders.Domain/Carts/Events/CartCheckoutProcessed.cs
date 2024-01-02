using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Services.Orders.Domain.Carts.Events;

public record CartCheckoutProcessed(Guid UserId) : IDomainEvent;
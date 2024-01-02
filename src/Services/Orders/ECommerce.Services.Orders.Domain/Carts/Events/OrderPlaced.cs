using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Domain.Carts.Events;

public record OrderPlaced(CheckoutCart CheckoutCart, DateTime Now, AggregateId? Id = null) : IDomainEvent;
using ECommerce.Services.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Services.Orders.Domain.Carts.Events;

public record CartCheckedOut(Cart Cart, List<CheckoutCartItem> Items) : IDomainEvent;
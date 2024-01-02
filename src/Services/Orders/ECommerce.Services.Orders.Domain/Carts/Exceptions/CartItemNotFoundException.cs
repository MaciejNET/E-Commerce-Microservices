using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Carts.Exceptions;

public sealed class CartItemNotFoundException : ECommerceException
{
    public CartItemNotFoundException(Guid id) : base($"Cart item with product ID: '{id}' was not found.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
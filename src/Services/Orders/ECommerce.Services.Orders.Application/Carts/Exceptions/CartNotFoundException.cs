using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class CartNotFoundException : ECommerceException
{
    public CartNotFoundException(Guid userId) : base($"Cart for user with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
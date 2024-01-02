using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class CartNotCheckedOutException : ECommerceException
{
    public CartNotCheckedOutException(Guid id) : base($"User with ID: '{id}' does not have active checkout.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
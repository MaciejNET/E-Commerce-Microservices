using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class CartAlreadyCheckedOutException : ECommerceException
{
    public CartAlreadyCheckedOutException(Guid id) : base($"User with ID: '{id}' already checkout cart.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
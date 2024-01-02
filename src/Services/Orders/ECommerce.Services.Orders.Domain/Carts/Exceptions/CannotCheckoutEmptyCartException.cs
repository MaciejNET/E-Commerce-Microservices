using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Carts.Exceptions;

public sealed class CannotCheckoutEmptyCartException : ECommerceException
{
    public CannotCheckoutEmptyCartException() : base("Empty cart cannot be checkout.")
    {
    }
}
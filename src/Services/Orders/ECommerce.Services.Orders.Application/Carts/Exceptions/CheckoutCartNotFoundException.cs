using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class CheckoutCartNotFoundException : ECommerceException
{
    public CheckoutCartNotFoundException(Guid userId) : base(
        $"Checkout cart for user with ID: {userId} does not exists.")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
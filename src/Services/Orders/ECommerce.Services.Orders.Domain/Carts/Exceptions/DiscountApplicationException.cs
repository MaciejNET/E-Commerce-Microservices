using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Carts.Exceptions;

public sealed class DiscountApplicationException : ECommerceException
{
    public DiscountApplicationException() : base(
        "Discount cannot be applied to products that are not in the item list.")
    {
    }
}
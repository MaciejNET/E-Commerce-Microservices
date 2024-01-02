using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class InvalidDiscountDateException : ECommerceException
{
    public InvalidDiscountDateException() : base("Invalid discount date.")
    {
    }
}
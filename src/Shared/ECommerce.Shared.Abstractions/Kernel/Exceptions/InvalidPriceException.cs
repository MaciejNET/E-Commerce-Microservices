using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Shared.Abstractions.Kernel.Exceptions;

public sealed class InvalidPriceException : ECommerceException
{
    public InvalidPriceException() : base("Price cannot be lower than 0.")
    {
    }
}
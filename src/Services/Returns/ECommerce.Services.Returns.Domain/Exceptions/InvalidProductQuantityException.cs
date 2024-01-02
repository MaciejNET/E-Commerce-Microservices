using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Returns.Domain.Exceptions;

public sealed class InvalidProductQuantityException : ECommerceException
{
    public InvalidProductQuantityException()
        : base("Product quantity must be equal or grater that 0.")
    {
    }
}
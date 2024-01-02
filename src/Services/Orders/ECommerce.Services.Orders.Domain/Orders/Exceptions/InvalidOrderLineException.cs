using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Orders.Exceptions;

public sealed class InvalidOrderLineException : ECommerceException
{
    public InvalidOrderLineException(string property) : base($"Invalid {property} in order line.")
    {
        Property = property;
    }

    public string Property { get; }
}
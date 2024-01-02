using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Shared.Exceptions;

public sealed class InvalidShipmentException : ECommerceException
{
    public InvalidShipmentException(string property) : base($"{property} cannot be null or empty.")
    {
        Property = property;
    }

    public string Property { get; }
}
using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Domain.Orders.Exceptions;

public sealed class NotEnoughProductsInStockException : ECommerceException
{
    public NotEnoughProductsInStockException(Guid productId)
        : base($"There is not enough stock units for product with ID: '{productId}'.")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
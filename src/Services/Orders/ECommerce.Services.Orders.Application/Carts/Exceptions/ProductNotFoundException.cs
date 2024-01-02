using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class ProductNotFoundException : ECommerceException
{
    public ProductNotFoundException(Guid productId) : base($"Product with ID: '{productId}' was not found.")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
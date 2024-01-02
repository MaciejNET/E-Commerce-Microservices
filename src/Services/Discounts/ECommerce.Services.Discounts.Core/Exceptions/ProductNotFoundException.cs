using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class ProductNotFoundException : ECommerceException
{
    public ProductNotFoundException(Guid id) : base($"Product with ID: '{id}' was not found.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
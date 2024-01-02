using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class ProductDiscountNotFoundException : ECommerceException
{
    public ProductDiscountNotFoundException(Guid id) : base($"Product discount with ID: '{id}' was not found.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
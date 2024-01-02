using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class ProductAlreadyHasDiscountException : ECommerceException
{
    public ProductAlreadyHasDiscountException(Guid id) : base(
        $"Product with ID: '{id}' already has discount in given date.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
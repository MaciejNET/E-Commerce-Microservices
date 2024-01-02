using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class DiscountCodeNotFoundException : ECommerceException
{
    public DiscountCodeNotFoundException(Guid id) : base($"Discount code with ID: '{id}' was not found.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
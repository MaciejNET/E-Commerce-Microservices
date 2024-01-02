using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Discounts.Core.Exceptions;

internal sealed class DiscountCodeAlreadyExistsException : ECommerceException
{
    public DiscountCodeAlreadyExistsException(string code) : base($"Discount code: '{code}' already exists.")
    {
        Code = code;
    }

    public string Code { get; }
}
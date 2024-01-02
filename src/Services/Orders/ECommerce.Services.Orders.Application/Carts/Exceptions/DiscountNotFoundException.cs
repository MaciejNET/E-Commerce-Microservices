using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Orders.Application.Carts.Exceptions;

public sealed class DiscountNotFoundException : ECommerceException
{
    public DiscountNotFoundException(string code) : base($"Discount with code: '{code}' was not found.")
    {
        Code = code;
    }

    public string Code { get; }
}
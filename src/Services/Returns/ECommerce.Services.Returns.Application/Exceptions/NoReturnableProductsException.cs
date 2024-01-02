using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Returns.Application.Exceptions;

public sealed class NoReturnableProductsException : ECommerceException
{
    public NoReturnableProductsException(string sku) : base($"No returnable products found for the SKU: {sku}.")
    {
        Sku = sku;
    }

    public string Sku { get; }
}
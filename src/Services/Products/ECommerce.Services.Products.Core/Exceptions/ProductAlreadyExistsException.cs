using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Products.Core.Exceptions;

internal sealed class ProductAlreadyExistsException : ECommerceException
{
    public ProductAlreadyExistsException(string sku) : base($"Product with SKU: {sku} already exists.")
    {
        Sku = sku;
    }

    public string Sku { get; }
}
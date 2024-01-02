using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Products.Core.Exceptions;

internal sealed class CategoryNotFoundException : ECommerceException
{
    public CategoryNotFoundException(string name) : base($"Category with ID: '{name}' was not found.")
    {
        Name = name;
    }

    public string Name { get; }
}
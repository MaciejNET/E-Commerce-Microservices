using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Products.Core.Exceptions;

internal sealed class CategoryAlreadyExistsException : ECommerceException
{
    public CategoryAlreadyExistsException(string name) : base($"Category with name: {name} already exists.")
    {
        Name = name;
    }

    public string Name { get; }
}
using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Reviews.Core.Exceptions;

internal class ProductNotFoundException : ECommerceException
{
    public ProductNotFoundException(Guid id) : base($"Product with ID: '{id}' does not exists.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
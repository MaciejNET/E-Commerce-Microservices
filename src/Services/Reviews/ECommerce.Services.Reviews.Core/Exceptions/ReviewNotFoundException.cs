using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Reviews.Core.Exceptions;

internal class ReviewNotFoundException : ECommerceException
{
    public ReviewNotFoundException(Guid id) : base($"Review with ID: '{id}' does not exists.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
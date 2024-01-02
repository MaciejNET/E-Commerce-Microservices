using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Reviews.Core.Exceptions;

internal sealed class ReviewDeleteAuthorizationException : ECommerceException
{
    public ReviewDeleteAuthorizationException(Guid id) : base($"User is not authorized to delete review with ID: {id}")
    {
        Id = id;
    }

    public Guid Id { get; }
}
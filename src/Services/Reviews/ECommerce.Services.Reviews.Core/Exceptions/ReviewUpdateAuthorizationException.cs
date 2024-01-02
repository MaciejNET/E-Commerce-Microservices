using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Reviews.Core.Exceptions;

internal sealed class ReviewUpdateAuthorizationException : ECommerceException
{
    public ReviewUpdateAuthorizationException(Guid id) : base($"User is not authorized to update review with ID: {id}")
    {
        Id = id;
    }

    public Guid Id { get; }
}
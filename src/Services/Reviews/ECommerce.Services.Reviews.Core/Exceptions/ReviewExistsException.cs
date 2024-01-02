using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Reviews.Core.Exceptions;

internal class ReviewExistsException : ECommerceException
{
    public ReviewExistsException(string email) : base($"User with email: '{email} already placed review.'")
    {
        Email = email;
    }

    public ReviewExistsException(Guid userId) : base($"User with ID: '{userId} already placed review.'")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
    public string Email { get; }
}
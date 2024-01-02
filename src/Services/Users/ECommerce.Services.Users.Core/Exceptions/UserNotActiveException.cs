using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Users.Core.Exceptions;

internal class UserNotActiveException : ECommerceException
{
    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
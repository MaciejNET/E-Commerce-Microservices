using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Users.Core.Exceptions;

internal sealed class InvalidPasswordException : ECommerceException
{
    public InvalidPasswordException() : base(
        "Password is invalid. It must contain at least one uppercase letter, one lowercase letter, one digit, one special character and 8 digits.")
    {
    }
}
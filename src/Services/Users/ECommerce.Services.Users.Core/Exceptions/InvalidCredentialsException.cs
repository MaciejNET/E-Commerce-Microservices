using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Users.Core.Exceptions;

internal class InvalidCredentialsException : ECommerceException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}
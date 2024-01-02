using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Users.Core.Exceptions;

internal class EmailInUseException : ECommerceException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}
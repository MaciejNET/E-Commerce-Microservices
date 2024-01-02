namespace ECommerce.Shared.Abstractions.Exceptions;

public abstract class ECommerceException : Exception
{
    protected ECommerceException(string message) : base(message)
    {
    }
}
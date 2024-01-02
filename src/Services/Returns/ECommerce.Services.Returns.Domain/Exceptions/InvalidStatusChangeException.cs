using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Returns.Domain.Exceptions;

public sealed class InvalidStatusChangeException : ECommerceException
{
    public InvalidStatusChangeException(string previousStatus, string nextStatus)
        : base($"Cannot change from '{previousStatus}' to '{nextStatus}'.")
    {
        PreviousStatus = previousStatus;
        NextStatus = nextStatus;
    }

    public string PreviousStatus { get; }
    public string NextStatus { get; }
}
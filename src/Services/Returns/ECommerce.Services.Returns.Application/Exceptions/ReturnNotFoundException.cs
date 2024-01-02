using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Returns.Application.Exceptions;

public sealed class ReturnNotFoundException : ECommerceException
{
    public ReturnNotFoundException(Guid id) : base($"Return with ID: '{id}' does not exists.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
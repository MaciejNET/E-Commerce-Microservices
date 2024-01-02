using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Services.Returns.Application.Exceptions;

public sealed class OrderNotFoundException : ECommerceException
{
    public OrderNotFoundException(Guid id) : base($"Order with ID: '{id}' does not exists.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
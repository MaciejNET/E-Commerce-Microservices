using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Services.Returns.Domain.Events;

public record ReturnStatusChanged(Guid OrderId, Guid OrderProductId, ReturnStatus Status) : IDomainEvent;
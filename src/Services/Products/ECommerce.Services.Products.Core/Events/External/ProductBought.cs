using Convey.CQRS.Events;
using Convey.MessageBrokers;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Services.Products.Core.Events.External;

[Message("orders")]
public record ProductBought(Guid ProductId, int Quantity) : IEvent, IDomainEvent;
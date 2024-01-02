using Convey.CQRS.Events;
using Convey.MessageBrokers;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("products")]
public record ProductUpdated(Guid Id, string Name, string Sku, Price Price, int StockQuantity) : IEvent;
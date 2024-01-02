using Convey.CQRS.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Products.Core.Events;

public record ProductCreated(Guid Id, string Name, string Sku, Price Price, int StockQuantity) : IEvent;
using Convey.CQRS.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Products.Core.Events;

public record ProductUpdated(Guid Id, string Name, string Sku, Price Price, int StockQuantity) : IEvent;
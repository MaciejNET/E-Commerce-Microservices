using Convey.CQRS.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Discounts.Core.Events;

internal record ProductDiscountAdded(Guid ProductId, Price NewPrice) : IEvent;
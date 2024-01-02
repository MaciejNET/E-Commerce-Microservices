using Convey.CQRS.Events;

namespace ECommerce.Services.Discounts.Core.Events;

internal record ProductDiscountExpired(Guid ProductId) : IEvent;
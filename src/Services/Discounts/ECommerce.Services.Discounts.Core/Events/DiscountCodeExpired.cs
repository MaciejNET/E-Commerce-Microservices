using Convey.CQRS.Events;

namespace ECommerce.Services.Discounts.Core.Events;

internal record DiscountCodeExpired(Guid Id) : IEvent;
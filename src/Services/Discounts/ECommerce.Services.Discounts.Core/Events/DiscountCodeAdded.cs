using Convey.CQRS.Events;

namespace ECommerce.Services.Discounts.Core.Events;

internal record DiscountCodeAdded(Guid Id, string Code, int Percentage, List<Guid> ProductIds) : IEvent;
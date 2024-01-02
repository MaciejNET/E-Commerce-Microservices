using Convey.CQRS.Events;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Users.Core.Events;

internal record SignedUp(Guid UserId, string Email, Currency PreferredCurrency) : IEvent;
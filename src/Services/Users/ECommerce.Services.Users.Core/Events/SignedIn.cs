using Convey.CQRS.Events;

namespace ECommerce.Services.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;
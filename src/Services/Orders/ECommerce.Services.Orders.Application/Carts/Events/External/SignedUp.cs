using Convey.CQRS.Events;
using Convey.MessageBrokers;
using ECommerce.Shared.Abstractions.Kernel.Enums;

namespace ECommerce.Services.Orders.Application.Carts.Events.External;

[Message("users")]
public record SignedUp(Guid UserId, string Email, Currency PreferredCurrency) : IEvent;
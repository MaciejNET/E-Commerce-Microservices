using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Domain.Shared.Enums;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record AddPayment(Guid UserId, PaymentMethod PaymentMethod) : ICommand;
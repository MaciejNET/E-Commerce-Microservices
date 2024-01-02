using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record ApplyDiscount(Guid UserId, string Code) : ICommand;
using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record DecreaseCartItemQuantity(Guid Id, int Quantity) : ICommand;
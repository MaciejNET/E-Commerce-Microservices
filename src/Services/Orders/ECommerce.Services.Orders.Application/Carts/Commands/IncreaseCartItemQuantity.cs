using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record IncreaseCartItemQuantity(Guid Id, int Quantity) : ICommand;
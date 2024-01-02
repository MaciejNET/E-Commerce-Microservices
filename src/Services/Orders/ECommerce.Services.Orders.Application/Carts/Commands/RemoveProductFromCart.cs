using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record RemoveProductFromCart(Guid UserId, Guid ProductId) : ICommand;
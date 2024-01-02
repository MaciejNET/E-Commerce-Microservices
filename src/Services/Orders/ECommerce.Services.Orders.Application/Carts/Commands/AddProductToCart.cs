using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record AddProductToCart(Guid UserId, Guid ProductId, int Quantity) : ICommand;
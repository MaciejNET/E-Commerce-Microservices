using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record ClearCart(Guid UserId) : ICommand;
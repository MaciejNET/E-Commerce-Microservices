using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Domain.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class DecreaseCartItemQuantityHandler : ICommandHandler<DecreaseCartItemQuantity>
{
    private readonly ICartItemRepository _cartItemRepository;

    public DecreaseCartItemQuantityHandler(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task HandleAsync(DecreaseCartItemQuantity command, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetAsync(new EntityId(command.Id));

        if (cartItem is null) throw new CartItemNotFoundException(command.Id);

        cartItem.DecreaseQuantity(command.Quantity);
        await _cartItemRepository.UpdateAsync(cartItem);
    }
}
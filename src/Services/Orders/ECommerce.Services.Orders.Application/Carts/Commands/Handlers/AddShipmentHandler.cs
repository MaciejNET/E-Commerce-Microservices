using Convey.CQRS.Commands;
using ECommerce.Services.Orders.Application.Carts.Exceptions;
using ECommerce.Services.Orders.Domain.Carts.Repositories;
using ECommerce.Services.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.Commands.Handlers;

public sealed class AddShipmentHandler : ICommandHandler<AddShipment>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public AddShipmentHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(AddShipment command, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null) throw new CartNotCheckedOutException(command.UserId);

        var shipment = new Shipment(command.City, command.StreetName, command.StreetNumber, command.ReceiverFullName);

        checkoutCart.SetShipment(shipment);

        await _checkoutCartRepository.UpdateAsync(checkoutCart);
    }
}
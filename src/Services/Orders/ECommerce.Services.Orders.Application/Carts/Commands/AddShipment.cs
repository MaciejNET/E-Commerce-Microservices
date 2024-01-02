using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Carts.Commands;

public record AddShipment(Guid UserId, string City, string StreetName, int StreetNumber, string ReceiverFullName)
    : ICommand;
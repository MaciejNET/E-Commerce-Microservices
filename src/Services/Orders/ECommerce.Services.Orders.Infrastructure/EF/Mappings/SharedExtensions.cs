using ECommerce.Services.Orders.Application.Shared.DTO;
using ECommerce.Services.Orders.Domain.Shared.ValueObjects;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class SharedExtensions
{
    internal static ShipmentDto AsDto(this Shipment shipment)
    {
        return new ShipmentDto(
            shipment.City,
            shipment.StreetName,
            shipment.StreetNumber,
            shipment.ReceiverFullName
        );
    }
}
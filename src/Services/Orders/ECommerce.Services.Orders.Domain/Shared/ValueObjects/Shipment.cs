using ECommerce.Services.Orders.Domain.Shared.Exceptions;

namespace ECommerce.Services.Orders.Domain.Shared.ValueObjects;

public record Shipment
{
    public Shipment(string city, string streetName, int streetNumber, string receiverFullName)
    {
        if (string.IsNullOrWhiteSpace(city)) throw new InvalidShipmentException(nameof(City));

        if (string.IsNullOrWhiteSpace(streetName)) throw new InvalidShipmentException(nameof(StreetName));

        if (string.IsNullOrWhiteSpace(receiverFullName)) throw new InvalidShipmentException(nameof(ReceiverFullName));

        City = city;
        StreetName = streetName;
        StreetNumber = streetNumber;
        ReceiverFullName = receiverFullName;
    }

    public string City { get; private set; }
    public string StreetName { get; private set; }
    public int StreetNumber { get; private set; }
    public string ReceiverFullName { get; private set; }
}
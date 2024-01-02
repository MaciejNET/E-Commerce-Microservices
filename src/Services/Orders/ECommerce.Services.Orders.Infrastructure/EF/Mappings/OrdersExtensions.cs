using ECommerce.Services.Orders.Application.Orders.DTO;
using ECommerce.Services.Orders.Domain.Orders.Entities;
using ECommerce.Services.Orders.Domain.Orders.ValueObjects;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Mappings;

public static class OrdersExtensions
{
    internal static OrderLineDto AsDto(this OrderLine line)
    {
        return new OrderLineDto(
            line.OrderLineNumber,
            line.Sku,
            line.Name,
            line.UnitPrice,
            line.Quantity
        );
    }

    public static OrderDto AsDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.UserId,
            order.Lines.Select(AsDto),
            order.Shipment.AsDto(),
            order.PaymentMethod,
            order.PlaceDate,
            order.Status
        );
    }
}
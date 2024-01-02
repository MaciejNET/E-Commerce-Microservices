using ECommerce.Services.Orders.Application.Shared.DTO;
using ECommerce.Services.Orders.Domain.Shared.Enums;

namespace ECommerce.Services.Orders.Application.Carts.DTO;

public record CheckoutCartDto(
    Guid Id,
    PaymentMethod PaymentMethod,
    ShipmentDto Shipment,
    DiscountDto Discount,
    IEnumerable<CheckoutCartItemDto> CartItems);
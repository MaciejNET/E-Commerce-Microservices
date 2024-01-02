namespace ECommerce.Services.Orders.Application.Carts.DTO;

public record CartDto(Guid Id, IEnumerable<CartItemDto> CartItems);
namespace ECommerce.Services.Orders.Application.Carts.DTO;

public record CartItemDto(Guid Id, int Quantity, ProductDto Product);
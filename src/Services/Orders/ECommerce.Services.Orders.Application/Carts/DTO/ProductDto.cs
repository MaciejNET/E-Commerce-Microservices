using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Services.Orders.Application.Carts.DTO;

public record ProductDto(Guid Id, string Name, string Sku, Price StandardPrice, Price? DiscountedPrice);
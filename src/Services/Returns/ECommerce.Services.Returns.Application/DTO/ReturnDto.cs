using ECommerce.Services.Returns.Domain.Entities;

namespace ECommerce.Services.Returns.Application.DTO;

public record ReturnDto(Guid Id, Guid UserId, Guid OrderId, string Sku, ReturnType Type, ReturnStatus Status);
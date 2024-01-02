using Convey.CQRS.Commands;
using ECommerce.Services.Returns.Domain.Entities;

namespace ECommerce.Services.Returns.Application.Commands;

public record ReturnProduct(Guid UserId, Guid OrderId, string Sku, ReturnType Type) : ICommand;
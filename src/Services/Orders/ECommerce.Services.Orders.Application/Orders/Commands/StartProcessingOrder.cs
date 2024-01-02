using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Orders.Commands;

public record StartProcessingOrder(Guid Id) : ICommand;
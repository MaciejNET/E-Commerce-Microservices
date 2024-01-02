using Convey.CQRS.Commands;

namespace ECommerce.Services.Orders.Application.Orders.Commands;

public record SendOrder(Guid Id) : ICommand;
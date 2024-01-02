using Convey.CQRS.Commands;

namespace ECommerce.Services.Returns.Application.Commands;

public record DeclineReturn(Guid Id) : ICommand;
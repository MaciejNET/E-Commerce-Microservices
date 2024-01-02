using Convey.CQRS.Commands;

namespace ECommerce.Services.Returns.Application.Commands;

public record AcceptReturn(Guid Id) : ICommand;
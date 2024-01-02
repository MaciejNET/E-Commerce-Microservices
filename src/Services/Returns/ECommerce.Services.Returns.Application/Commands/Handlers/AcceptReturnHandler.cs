using Convey.CQRS.Commands;
using ECommerce.Services.Returns.Application.Exceptions;
using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Services.Returns.Domain.Repositories;

namespace ECommerce.Services.Returns.Application.Commands.Handlers;

internal sealed class AcceptReturnHandler : ICommandHandler<AcceptReturn>
{
    private readonly IReturnRepository _returnRepository;

    public AcceptReturnHandler(IReturnRepository returnRepository)
    {
        _returnRepository = returnRepository;
    }

    public async Task HandleAsync(AcceptReturn command, CancellationToken cancellationToken)
    {
        var @return = await _returnRepository.GetAsync(command.Id);

        if (@return is null) throw new ReturnNotFoundException(command.Id);

        @return.ChangeStatus(ReturnStatus.Accepted);
        await _returnRepository.UpdateAsync(@return);
    }
}
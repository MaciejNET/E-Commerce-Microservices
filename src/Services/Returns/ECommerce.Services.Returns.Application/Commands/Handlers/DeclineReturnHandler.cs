using Convey.CQRS.Commands;
using ECommerce.Services.Returns.Application.Exceptions;
using ECommerce.Services.Returns.Domain.Entities;
using ECommerce.Services.Returns.Domain.Repositories;

namespace ECommerce.Services.Returns.Application.Commands.Handlers;

internal sealed class DeclineReturnHandler : ICommandHandler<DeclineReturn>
{
    private readonly IReturnRepository _returnRepository;

    public DeclineReturnHandler(IReturnRepository returnRepository)
    {
        _returnRepository = returnRepository;
    }

    public async Task HandleAsync(DeclineReturn command, CancellationToken cancellationToken)
    {
        var @return = await _returnRepository.GetAsync(command.Id);

        if (@return is null) throw new ReturnNotFoundException(command.Id);

        @return.ChangeStatus(ReturnStatus.Declined);
        await _returnRepository.UpdateAsync(@return);
    }
}
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using ECommerce.Services.Returns.Application.Commands;
using ECommerce.Services.Returns.Application.DTO;
using ECommerce.Services.Returns.Application.Queries;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Returns.Api.Controllers;

[Authorize(Policy = Policy)]
internal class ReturnController : BaseController
{
    private const string Policy = "returns";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;
    private readonly IQueryDispatcher _queryDispatcher;

    public ReturnController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReturnDto>>> GetUserReturns()
    {
        var id = _context.Identity.Id;

        return Ok(await _queryDispatcher.QueryAsync(new GetUserReturns {Id = id}));
    }

    [HttpGet("to-check")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<IEnumerable<ReturnDto>>> GetReturnsToCheck()
    {
        return Ok(await _queryDispatcher.QueryAsync(new GetReturnsToCheck()));
    }

    [HttpPost]
    public async Task<ActionResult> Return(ReturnProduct command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }

    [HttpPost("accept")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Accept(AcceptReturn command)
    {
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }

    [HttpPost("decline")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Decline(DeclineReturn command)
    {
        await _commandDispatcher.SendAsync(command);

        return Ok();
    }
}
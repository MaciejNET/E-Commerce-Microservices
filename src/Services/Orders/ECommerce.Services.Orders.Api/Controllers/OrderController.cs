using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Orders.Commands;
using ECommerce.Services.Orders.Application.Orders.DTO;
using ECommerce.Services.Orders.Application.Orders.Queries;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Orders.Api.Controllers;

[Authorize(Policy = Policy)]
internal class OrderController : BaseController
{
    private const string Policy = "orders";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;
    private readonly IQueryDispatcher _queryDispatcher;

    public OrderController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Get(Guid id)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOrder {Id = id}));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> Browse(bool? isCompleted)
    {
        var role = _context.Identity.Role;

        var query = new BrowseOrders
        {
            UserId = role == "user" ? _context.Identity.Id : null,
            IsCompleted = isCompleted
        };

        return Ok(await _queryDispatcher.QueryAsync(query));
    }

    [HttpPost("{id:guid}/start-processing")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> StartProcessing(Guid id)
    {
        await _commandDispatcher.SendAsync(new StartProcessingOrder(id));

        return Ok();
    }

    [HttpPost("{id:guid}/send")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Send(Guid id)
    {
        await _commandDispatcher.SendAsync(new SendOrder(id));

        return Ok();
    }

    [HttpPost("{id:guid}/complete")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Complete(Guid id)
    {
        await _commandDispatcher.SendAsync(new CompleteOrder(id));

        return Ok();
    }

    [HttpPost("{id:guid}/cancel")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        await _commandDispatcher.SendAsync(new CancelOrder(id));

        return Ok();
    }
}
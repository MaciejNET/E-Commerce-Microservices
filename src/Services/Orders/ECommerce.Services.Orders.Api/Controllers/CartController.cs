using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Carts.Commands;
using ECommerce.Services.Orders.Application.Carts.DTO;
using ECommerce.Services.Orders.Application.Carts.Queries;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Orders.Api.Controllers;

[Authorize(Policy = Policy)]
internal class CartController : BaseController
{
    private const string Policy = "carts";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;
    private readonly IQueryDispatcher _queryDispatcher;

    public CartController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDto>> Get()
    {
        var id = _context.Identity.Id;

        return await _queryDispatcher.QueryAsync(new GetCart {UserId = id});
    }

    [HttpPost("add-product")]
    public async Task<ActionResult> AddProduct(AddProductToCart command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("remove-product")]
    public async Task<ActionResult> RemoveProduct(RemoveProductFromCart command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut("{cartItemId:guid}/increase-quantity")]
    public async Task<ActionResult> IncreaseQuantity(Guid cartItemId, int quantity)
    {
        var command = new IncreaseCartItemQuantity(cartItemId, quantity);

        await _commandDispatcher.SendAsync(command);

        return NoContent();
    }

    [HttpPut("{cartItemId:guid}/decrease-quantity")]
    public async Task<ActionResult> DecreaseQuantity(Guid cartItemId, int quantity)
    {
        var command = new DecreaseCartItemQuantity(cartItemId, quantity);

        await _commandDispatcher.SendAsync(command);

        return NoContent();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult> Checkout()
    {
        var id = _context.Identity.Id;
        var command = new CheckoutCart(id);
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut("clear")]
    public async Task<ActionResult> Clear()
    {
        var id = _context.Identity.Id;
        var command = new ClearCart(id);
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}
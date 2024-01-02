using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using ECommerce.Services.Orders.Application.Carts.Commands;
using ECommerce.Services.Orders.Application.Carts.DTO;
using ECommerce.Services.Orders.Application.Carts.Queries;
using ECommerce.Services.Orders.Domain.Shared.Enums;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Orders.Api.Controllers;

[Authorize(Policy = Policy)]
internal class CheckoutCartController : BaseController
{
    private const string Policy = "carts";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IContext _context;
    private readonly IQueryDispatcher _queryDispatcher;

    public CheckoutCartController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,
        IContext context)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CheckoutCartDto>> Get()
    {
        var id = _context.Identity.Id;

        return await _queryDispatcher.QueryAsync(new GetCheckoutCart {UserId = id});
    }

    [HttpPost("add-payment")]
    public async Task<ActionResult> AddPayment(PaymentMethod paymentMethod)
    {
        var id = _context.Identity.Id;
        await _commandDispatcher.SendAsync(new AddPayment(id, paymentMethod));
        return Ok();
    }

    [HttpPost("add-shipment")]
    public async Task<ActionResult> AddShipment(AddShipment command)
    {
        var id = _context.Identity.Id;
        command = command with {UserId = id};
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("apply-discount")]
    public async Task<ActionResult> ApplyDiscount(string code)
    {
        var id = _context.Identity.Id;
        await _commandDispatcher.SendAsync(new ApplyDiscount(id, code));
        return Ok();
    }

    [HttpPost("place-order")]
    public async Task<ActionResult> PlaceOrder()
    {
        var id = _context.Identity.Id;
        await _commandDispatcher.SendAsync(new PlaceOrder(id));
        return Ok();
    }

    [HttpDelete("cancel-checkout")]
    public async Task<ActionResult> CancelCheckout()
    {
        var id = _context.Identity.Id;
        await _commandDispatcher.SendAsync(new CancelCheckout(id));
        return Ok();
    }
}
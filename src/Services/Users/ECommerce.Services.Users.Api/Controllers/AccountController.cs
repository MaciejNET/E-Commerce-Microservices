using ECommerce.Services.Users.Core.DTO;
using ECommerce.Services.Users.Core.Services;
using ECommerce.Shared.Abstractions.Auth;
using ECommerce.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Users.Api.Controllers;

internal class AccountController : BaseController
{
    private readonly IContext _context;
    private readonly IIdentityService _identityService;

    public AccountController(IIdentityService identityService, IContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<AccountDto>> GetAsync()
    {
        return OkOrNotFound(await _identityService.GetAsync(_context.Identity.Id));
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUpAsync(SignUpDto dto)
    {
        await _identityService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JsonWebToken>> SignInAsync(SignInDto dto)
    {
        return Ok(await _identityService.SignInAsync(dto));
    }
}
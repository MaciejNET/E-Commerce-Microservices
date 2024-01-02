using ECommerce.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Users.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(UsersService.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null) return NotFound();

        return Ok(model);
    }

    protected void AddResourceIdHeader(Guid id)
    {
        Response.Headers.Add("Resource-ID", id.ToString());
    }
}
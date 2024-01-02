using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Users.Api.Controllers;

[Route(UsersService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Users API";
    }
}
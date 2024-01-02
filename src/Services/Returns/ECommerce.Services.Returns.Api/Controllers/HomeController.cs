using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Returns.Api.Controllers;

[Route(ReturnsService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Returns API";
    }
}
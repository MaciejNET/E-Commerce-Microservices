using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Discounts.Api.Controllers;

[Route(DiscountsService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Discounts API";
    }
}
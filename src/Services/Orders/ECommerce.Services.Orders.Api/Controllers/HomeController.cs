using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Orders.Api.Controllers;

[Route(OrdersService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Orders API";
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Products.Api.Controllers;

[Route(ProductsService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Product API";
    }
}
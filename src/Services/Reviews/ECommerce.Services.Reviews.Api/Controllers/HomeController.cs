using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Reviews.Api.Controllers;

[Route(ReviewsService.BasePath)]
internal class HomeController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Reviews API";
    }
}
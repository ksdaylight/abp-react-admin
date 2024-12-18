using Microsoft.AspNetCore.Mvc;

namespace Miwen.MicroService.Applications.Single.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("/swagger");
    }
}

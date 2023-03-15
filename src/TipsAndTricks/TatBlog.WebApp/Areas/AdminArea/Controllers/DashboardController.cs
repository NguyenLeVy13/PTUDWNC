using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

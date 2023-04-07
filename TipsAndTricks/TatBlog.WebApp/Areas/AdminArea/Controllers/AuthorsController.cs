using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

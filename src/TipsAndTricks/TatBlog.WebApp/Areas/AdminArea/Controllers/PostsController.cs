using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers
{
    public class TagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

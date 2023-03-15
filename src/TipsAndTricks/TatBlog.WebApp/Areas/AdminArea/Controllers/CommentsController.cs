using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

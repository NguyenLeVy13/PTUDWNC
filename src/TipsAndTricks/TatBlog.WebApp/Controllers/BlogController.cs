using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");

            return View();
        }

        public IActionResult About()
            => View();
        public IActionResult Contact()
            => View();
        public IActionResult Rss()
            => Content("Nội dung sẽ được cập nhật");
    }
}

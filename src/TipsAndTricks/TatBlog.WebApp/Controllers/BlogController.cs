using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IActionResult> Index(
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10)
        {
            //Tạo đối tượng chứa các đk truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true
            };

            //Truy vấn các bài viết theo đk đã tạo
            var postsList = await _blogRepository
                .GetPagedPostQueryAsync(postQuery, pageNumber ,pageSize);

            //Lưu lại đk truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            //Truyền ds bài viết vào View để render ra HTML
            return View(postsList);
        }

        public IActionResult About()
            => View();
        public IActionResult Contact()
            => View();
        public IActionResult Rss()
            => Content("Nội dung sẽ được cập nhật");
    }
}

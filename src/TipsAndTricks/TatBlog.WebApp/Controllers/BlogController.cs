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

        //Action này xử lý HTTP request đến trang chủ của
        //ứng dụng web hoặc tìm kiếm bài viết theo từ khoá
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10)
        {
            //Tạo đối tượng chứa các đk truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                //Tìm bài viết theo từ khóa
                Keyword = keyword
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.AdminArea.Models;

namespace TatBlog.WebApp.Areas.AdminArea.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public PostsController(IBlogRepository blogRepository) 
        {
            _blogRepository = blogRepository;
        }

        private async Task PopulatePostFileterModelAsync(PostFilterModel model)
        {
            var authors = await _blogRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(a => new SelectListItem()
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            });
            model.CategoryList = categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

        }
        public async Task<IActionResult> Index(PostFilterModel model)
        {
            var postQuery = new PostQuery()
            {
                Keyword = model.Keyword,
                CategoryId = model.CategoryId,
                AuthorId = model.AuthorId,
                Year = model.Year,
                Month = model.Month,
            };

            ViewBag.PostList = await _blogRepository
                .GetPagedPostsAsync(postQuery, 1, 10);

            await PopulatePostFileterModelAsync(model);

            return View(model);
        }
    }
}

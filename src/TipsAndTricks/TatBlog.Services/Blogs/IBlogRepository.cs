using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{
    //Tìm bài viết có tên định danh là 'slug'
    //và dc đăng vào tháng 'month' năm 'year'

    Task<Post> GetPostAsync(
        int year,
        int month,
        string slug,
        CancellationToken cancellationToken = default);

    //Tìm Top N bài viết phổ dc nhiều ng xem nhất
    Task<IList<Post>> GetPopulerArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default);

    //Kiểm tra xem tên định danh của bài viết đã có hay ch?
    Task<bool> IsPostSlugExistedAsync(
       int postId, string slug,
       CancellationToken cancellationToken = default);

    //Tăng số lượt xem của 1 bài viết
    Task IncreaseViewCountAsync(
       int postId,
       CancellationToken cancellationToken = default);

    Task<IList<CategoryItem>> GetCategoriesAsync(
         bool showOnMenu = false,
         CancellationToken cancellationToken = default);
  
    Task<IPagedList<TagItem>> GetPageTagsAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);
   
    Task<IPagedList<Post>> GetPagedPostQueryAsync(
            PostQuery postQuery,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper);
   Task<IList<AuthorItem>> GetAuthorsAsync(

   CancellationToken cancellationToken = default);
    Task<IPagedList<Post>> GetPagedPostsAsync(
        PostQuery condition,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}


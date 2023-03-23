using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Blogs;
using TatBlog.Services.Extensions;


namespace TatBlog.Services.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDbContext _context;

    public BlogRepository(BlogDbContext context)
    {
        _context = context;
    }

    //Tìm bài viết có tên định danh là 'slug'
    //và dc đăng vào tháng 'month' năm 'year'

    public async Task<Post> GetPostAsync(
        int year,
        int month,
        string slug,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Post> postsQuery = _context.Set<Post>()
             .Include(x => x.Category)
             .Include(x => x.Author);

        if (year > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
        }

        if (month > 0)
        {
            postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
        }

        if (!string.IsNullOrWhiteSpace(slug))
        {
            postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
        }

        return await postsQuery.FirstOrDefaultAsync(cancellationToken);
    }

    // Tìm Top N bài viết phổ dc nhiều ng nhất
    public async Task<IList<Post>> GetPopulerArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .ToListAsync(cancellationToken);
    }
    //Kiểm tra xem tên định danh của bài viết đã có hay ch?
    public async Task<bool> IsPostSlugExistedAsync(
       int postId, string slug,
       CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
             .AnyAsync(x => x.Id != postId && x.UrlSlug == slug,
                 cancellationToken);
    }
    //Tăng số lượt xem của 1 bài viết
    public async Task IncreaseViewCountAsync(
       int postId,
       CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(p =>
            p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
            cancellationToken);
    }

    public async Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();

        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu);
        }

        return await categories
            .OrderBy(x => x.Name)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<TagItem>> GetPageTagsAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Tag>()
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published)
            });
        return await tagQuery
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper)
    {
        var posts = FilterPosts(condition);
        var projectedPosts = mapper(posts);

        return await projectedPosts.ToPagedListAsync(pagingParams);
    }


    public async Task<IPagedList<Post>> GetPagedPostQueryAsync(
        PostQuery condition,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await FilterPosts(condition).ToPagedListAsync(
            pageNumber, pageSize,
            nameof(Post.PostedDate), "DESC",
            cancellationToken);
    }

    public async Task<IList<AuthorItem>> GetAuthorsAsync(

    CancellationToken cancellationToken = default)
    {
        IQueryable<Author> authors = _context.Set<Author>();


        return await authors
            .OrderBy(x => x.FullName)
            .Select(x => new AuthorItem()
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlSlug = x.UrlSlug,
                Email = x.Email,
                JoinedDate = x.JoinedDate,
                ImageUrl = x.ImageUrl,
                Notes = x.Notes,
                PostCount = x.Posts.Count(p => p.Published),
            })
            .ToListAsync(cancellationToken);
    }
    public async Task<IPagedList<Post>> GetPagedPostsAsync(
        PostQuery condition,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await FilterPosts(condition).ToPagedListAsync(
            pageNumber, pageSize,
            nameof(Post.PostedDate), "DESC",
            cancellationToken);
    }

    public async Task<Post> GetPostByIdAsync(
        int postId, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        if (!includeDetails)
        {
            return await _context.Set<Post>().FindAsync(postId);
        }

        return await _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);
    }

    private IQueryable<Post> FilterPosts(PostQuery condition)
    {
        IQueryable<Post> posts = _context.Set<Post>()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Include(x => x.Tags);

        if (condition.PublishedOnly)
        {
            posts = posts.Where(x => x.Published);
        }

        if (condition.NotPublished)
        {
            posts = posts.Where(x => !x.Published);
        }

        if (condition.CategoryId > 0)
        {
            posts = posts.Where(x => x.CategoryId == condition.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(condition.CategorySlug))
        {
            posts = posts.Where(x => x.Category.UrlSlug == condition.CategorySlug);
        }

        if (condition.AuthorId > 0)
        {
            posts = posts.Where(x => x.AuthorId == condition.AuthorId);
        }

        if (!string.IsNullOrWhiteSpace(condition.AuthorSlug))
        {
            posts = posts.Where(x => x.Author.UrlSlug == condition.AuthorSlug);
        }

        if (!string.IsNullOrWhiteSpace(condition.TagSlug))
        {
            posts = posts.Where(x => x.Tags.Any(t => t.UrlSlug == condition.TagSlug));
        }

        if (!string.IsNullOrWhiteSpace(condition.Keyword))
        {
            posts = posts.Where(x => x.Title.Contains(condition.Keyword) ||
                                     x.ShortDescription.Contains(condition.Keyword) ||
                                     x.Description.Contains(condition.Keyword) ||
                                     x.Category.Name.Contains(condition.Keyword) ||
                                     x.Tags.Any(t => t.Name.Contains(condition.Keyword)));
        }

        if (condition.Year > 0)
        {
            posts = posts.Where(x => x.PostedDate.Year == condition.Year);
        }

        if (condition.Month > 0)
        {
            posts = posts.Where(x => x.PostedDate.Month == condition.Month);
        }

        if (!string.IsNullOrWhiteSpace(condition.TitleSlug))
        {
            posts = posts.Where(x => x.UrlSlug == condition.TitleSlug);
        }

        return posts;
    }

    public async Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default)
    {
        if (post.Id > 0)
        {
            await _context.Entry(post).Collection(x => x.Tags).LoadAsync(cancellationToken);
        }
        else
        {
            post.Tags = new List<Tag>();
        }

        var validTags = tags.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => new
            {
                Name = x,
                Slug = x.GenerateSlug()
            })
            .GroupBy(x => x.Slug)
            .ToDictionary(g => g.Key, g => g.First().Name);


        foreach (var kv in validTags)
        {
            if (post.Tags.Any(x => string.Compare(x.UrlSlug, kv.Key, StringComparison.InvariantCultureIgnoreCase) == 0)) continue;

            var tag = await GetTagAsync(kv.Key, cancellationToken) ?? new Tag()
            {
                Name = kv.Value,
                Description = kv.Value,
                UrlSlug = kv.Key
            };

            post.Tags.Add(tag);
        }

        post.Tags = post.Tags.Where(t => validTags.ContainsKey(t.UrlSlug)).ToList();

        if (post.Id > 0)
            _context.Update(post);
        else
            _context.Add(post);

        await _context.SaveChangesAsync(cancellationToken);

        return post;
    }

    public async Task<bool> TogglePublishedFlagAsync(
        int postId, CancellationToken cancellationToken = default)
    {
        var post = await _context.Set<Post>().FindAsync(postId);

        if (post is null) return false;

        post.Published = !post.Published;
        
    }

    public async Task<Tag> GetTagAsync(
         string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Tag>()
            .FirstOrDefaultAsync(x => x.UrlSlug == slug, cancellationToken);
    }

    public async Task<bool> DeletePostAsync(
        int postId, CancellationToken cancellationToken = default)
    {
        var post = await _context.Set<Post>().FindAsync(postId);

        if(!post.Published) return false;

        _context.Set<Post>() .Remove(post);
        var rowCount = await _context.SaveChangesAsync(cancellationToken); 
        
        return rowCount > 0;
    }
}
    

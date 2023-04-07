using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;

namespace TatBlog.Core.Entities
{
    // Biểu diễn các chuyên mục hay chủ đề
    public class Category : IEntity
    {
        // Mã chuyên mục
        public int Id { get; set; }

        // Tên chuyên mục, chủ đề
        public string Name { get; set; }

        // Tên định danh dùng để tạo URL
        public string UrlSlug { get; set; }

        // Mô tả thêm về chuyên mục
        public string Description { get; set; }

        // Đánh dấu chuyên mục được hiển thị trên menu
        public bool ShowOnMenu { get; set; }

        // Danh sách các bài viết thuộc chuyên mục
        public IList<Post> Posts { get; set; }

        
    }
}

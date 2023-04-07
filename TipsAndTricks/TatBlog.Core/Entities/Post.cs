using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;

namespace TatBlog.Core.Entities
{
    // Biểu diễn một bài viết của blog
    public class Post : IEntity
    {
        //Mã bài viết 
         public int Id { get; set; }

        // Tiêu đề bài viết
        public string Title { get; set; }

        // Mô tả hay giới thiệu ngắn về nội dung
        public string ShortDescription { get; set; }

        //Nội dung chi tiết của bài viết 
        public string Description { get; set; }

        //Metadata
        public string Meta { get; set; }

        // Tên định danh để tạo URL
        public string UrlSlug { get; set; }

        // Đường dẫn đến tập tin hình ảnh 
        public string ImageUrl { get; set; }

        // Số lượt xem, đọc bài viết
        public int ViewCount { get; set; }

        // Trạng thái của bài viết
        public bool Published { get; set; }
        
        //Ngày giờ đăng bài
        public DateTime PostedDate { get; set; }

        //Ngày giờ cập nhật lần cuối
        public DateTime? ModifiedDate { get; set; }

        // Mã chuyên mục
        public int CategoryId { get; set; }

        // Mã tác giả của bài viết 
        public int AuthorId { get; set; }

        // Chuyên mục của bài viết
        public Category Category { get; set; }

        //Tác giả của bài viết
        public Author Author { get; set; }

        // Danh sách các từ khóa của bài viết
        public IList<Tag> Tags { get; set; }
    }
}

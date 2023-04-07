using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.DTO
{
    public class AuthorItem
    {
        // Mã tác giả bài viết
        public int Id { get; set; }

        //Tên tác giả
        public string FullName { get; set; }

        //Tên định danh dùng để tạo URL
        public string UrlSlug { get; set; }

        //Đường dẫn tới file hình ảnh
        public string ImageUrl { get; set; }

        //Ngày bắt đầu
        public DateTime JoinedDate { get; set; }

        //Địa chỉ email
        public string Email { get; set; }

        //Ghi chú
        public string Notes { get; set; }

        public int PostCount { get; set; }
    }
}

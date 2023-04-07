namespace TatBlog.WebApi.Models;

public class CategoryEditModel
{
    //Tên chuyên mục, chủ đề
    public string Name { get; set; }

    //Tên định dạng để tạo URL
    public string UrlSlug { get; set; }

    //Mô tả chuyên mục
    public string Description { get; set; }

    //Đánh giá chuyên mục dc hthi trên menu
    public bool ShowOnMenu { get; set; }
}

using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations;

public class CategoryValidator : AbstractValidator<CategoryEditModel>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Chủ đề không được để trống")
            .MaximumLength(100)
            .WithMessage("Chủ đề tối đa 100 ký tự");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("UrlSlug không được để trống");

        RuleFor(x => x.UrlSlug)
            .NotEmpty()
            .WithMessage("Slug không được để trống")
            .MaximumLength(100)
            .WithMessage("Slug tối đa 100 ký tự");
    }
}


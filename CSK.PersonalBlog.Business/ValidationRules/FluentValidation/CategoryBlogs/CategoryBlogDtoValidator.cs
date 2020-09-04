using CSK.PersonalBlog.DTO.DTOs.CategoryBlogDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.CategoryBlogs
{
    public class CategoryBlogDtoValidator : AbstractValidator<CategoryBlogDto>
    {
        public CategoryBlogDtoValidator()
        {
            RuleFor(i => i.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("Kategori id boş geçilemez.");
            RuleFor(i => i.BlogId).InclusiveBetween(0, int.MaxValue).WithMessage("Blog id boş geçilemez.");
        }
    }
}

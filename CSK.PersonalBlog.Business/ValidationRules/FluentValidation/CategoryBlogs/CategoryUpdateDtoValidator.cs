using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.CategoryBlogs
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(i => i.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Kategori id boş geçilemez.");

            RuleFor(i => i.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                                .Length(1, 50).WithMessage("Kategori adı en fazla 50 karakter içermelidir.");
        }
    }
}

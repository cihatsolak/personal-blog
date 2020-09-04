using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.CategoryBlogs
{
    public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                                .Length(1,50).WithMessage("Kategori adı en fazla 50 karakter içermelidir.");

        }
    }
}

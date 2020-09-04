using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using FluentValidation;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Validations
{
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(i => i.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                                .Length(1, 50).WithMessage("Kategori adı en fazla 50 karakter içermelidir.");
        }
    }
}

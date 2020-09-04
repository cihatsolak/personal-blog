using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using FluentValidation;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Validations
{
    public class BlogModelValidator : AbstractValidator<BlogModel>
    {
        public BlogModelValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Başlık boş geçilemez.")
                                 .Length(5, 50).WithMessage("Başlık en fazla 50 karakter içerebilir.");

            RuleFor(i => i.ShortDescription).NotEmpty().WithMessage("Kısa açıklama boş geçilemez.")
                                            .Length(1, 250).WithMessage("Kısa açıklama en fazla 250 karakter olmalıdır.");

            RuleFor(i => i.Description).NotEmpty().WithMessage("İçerik boş geçilemez.");

            RuleFor(i => i.ImageFile).NotEmpty().WithMessage("Lütfen bir görsel seçiniz.").When(i => i.Id == 0);
        }
    }
}

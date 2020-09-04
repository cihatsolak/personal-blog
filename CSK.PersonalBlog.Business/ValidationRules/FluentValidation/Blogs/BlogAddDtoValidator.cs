using CSK.PersonalBlog.DTO.DTOs.BlogDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.Blogs
{
    public class BlogAddDtoValidator : AbstractValidator<BlogAddDto>
    {
        public BlogAddDtoValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Başlık boş geçilemez.")
                                 .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter içerebilir.");

            RuleFor(i => i.ShortDescription).NotEmpty().WithMessage("Kısa açıklama boş geçilemez.")
                                            .Length(1, 250).WithMessage("Kısa açıklama en fazla 250 karakter olmalıdır.");

            RuleFor(i => i.Description).NotEmpty().WithMessage("İçerik boş geçilemez.");

            RuleFor(i => i.ImageFile).NotEmpty().WithMessage("Lütfen bir görsel seçiniz.");
        }
    }
}

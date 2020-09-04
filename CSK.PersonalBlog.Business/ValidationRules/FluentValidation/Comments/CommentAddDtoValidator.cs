using CSK.PersonalBlog.DTO.DTOs.CommentDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.Comments
{
    public class CommentAddDtoValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddDtoValidator()
        {
            RuleFor(i => i.AuthorName).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                                      .MinimumLength(5).WithMessage("Geçerli bir ad giriniz.");

            RuleFor(i => i.AuthorEmail).NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                                       .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(i => i.Description).NotEmpty().WithMessage("İçerik alanı boş bırakılamaz.")
                                       .MinimumLength(50).WithMessage("İçerik en az 50 karaktere sahip olmalıdır.");
        }
    }
}

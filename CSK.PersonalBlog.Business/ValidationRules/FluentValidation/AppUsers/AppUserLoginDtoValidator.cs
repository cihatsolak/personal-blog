using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using FluentValidation;

namespace CSK.PersonalBlog.Business.ValidationRules.FluentValidation.AppUsers
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(i => i.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                                               .Length(2, 50).WithMessage("Geçerli bir kullanıcı adı giriniz.");

            RuleFor(i => i.Password).NotEmpty().WithMessage("Şifre boş geçilemez.")
                                    .Length(2, 50).WithMessage("Geçerli bir şifre giriniz.");
        }
    }
}

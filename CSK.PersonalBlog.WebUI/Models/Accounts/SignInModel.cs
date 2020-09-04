using System.ComponentModel.DataAnnotations;

namespace CSK.PersonalBlog.WebUI.Models.Accounts
{
    public class SignInModel
    {
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

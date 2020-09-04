using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {

        private readonly IGenericApiService _genericApiManager;
        public AccountController(IGenericApiService genericApiManager)
        {
            _genericApiManager = genericApiManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            if (!ModelState.IsValid)
                return View(signInModel);

            var accessToken = await _genericApiManager.PostAsync<ResponseModel<AccessToken>>("Auth/SignIn", signInModel);

            if (accessToken != null)
            {
                HttpContext.Session.SetString("token", accessToken.Result.Token);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı adı ya da şifre yanlış");

            return View(signInModel);
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction(nameof(SignIn));
        }
    }
}

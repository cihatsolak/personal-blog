using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Accounts;
using CSK.PersonalBlog.WebUI.Tools.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CSK.PersonalBlog.WebUI.Tools.Filters
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("SignIn", "Account", null);
                return;
            }
            else
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var httpResponseMessage = httpClient.GetAsync("http://localhost:51765/api/v1/Auth/ActiveUser").Result;

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);
                    return;
                }

                var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var accountViewModel = JsonConvert.DeserializeObject<ResponseModel<AccountViewModel>>(responseContent);

                context.HttpContext.Session.SetObject("ActiveUser", accountViewModel.Result);
            }
        }
    }
}

using CSK.PersonalBlog.WebUI.Tools.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        [JwtAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

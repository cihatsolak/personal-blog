using CSK.PersonalBlog.WebUI.Tools.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize]
    public class BaseAdminController : Controller
    {
    }
}

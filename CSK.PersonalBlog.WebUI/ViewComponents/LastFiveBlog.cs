using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ViewComponents
{
    public class LastFiveBlog : ViewComponent
    {
        private readonly IGenericApiService _genericApiService;
        public LastFiveBlog(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _genericApiService.GetAsync<ResponseModel<List<BlogViewModel>>>("blogs/GetLastFiveBlog");
            return View(blogs.Result);
        }
    }
}

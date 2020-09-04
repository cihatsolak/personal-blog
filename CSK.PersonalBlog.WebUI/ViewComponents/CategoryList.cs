using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly IGenericApiService _genericApiService;
        public CategoryList(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _genericApiService.GetAsync<ResponseModel<List<CategoryWithBlogsCountViewModel>>>("categories/GetAllWithBlogsCount");
            return View(categories.Result);
        }
    }
}

using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Blogs;
using CSK.PersonalBlog.WebUI.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IGenericApiService _genericApiService;
        public BlogController(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }

        [HttpGet]
        public async Task<IActionResult> List(int categoryId = 0, string s = null)
        {
            TempData["categoryId"] = categoryId;
            TempData["search"] = s;

            ResponseModel<List<BlogViewModel>> blogs;
            
            if (0 >= categoryId && string.IsNullOrWhiteSpace(s))
                blogs = await _genericApiService.GetAsync<ResponseModel<List<BlogViewModel>>>("blogs/");
            else if (0 >= categoryId && !string.IsNullOrEmpty(s))
                blogs = await _genericApiService.GetAsync<ResponseModel<List<BlogViewModel>>>($"blogs/GetAllBySearch?search={s}");
            else
                blogs = await _genericApiService.GetAsync<ResponseModel<List<BlogViewModel>>>($"blogs/GetAllByCategoryId/{categoryId}");

            return View(blogs.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var responseComments = await _genericApiService.GetAsync<ResponseModel<List<CommentViewModel>>>($"blogs/GetComments/{id}");

            if (responseComments.StatusCode == 1)
            {
                ViewBag.Comments = responseComments.Result;
            }

            var blogViewModel = await _genericApiService.GetAsync<ResponseModel<BlogViewModel>>($"blogs/{id}");

            return View(blogViewModel.Result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToComment(CommentAddViewModel model)
        {
            if (await _genericApiService.PostAsync("blogs/AddToComment", model))
                return RedirectToAction(nameof(Detail), new { id = model.BlogId });

            return View(model);
        }
    }
}

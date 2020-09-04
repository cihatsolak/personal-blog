using AutoMapper;
using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Blogs;
using CSK.PersonalBlog.WebUI.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Controllers
{
    public class BlogController : BaseAdminController
    {
        private readonly IBlogApiService _blogApiService;
        private readonly IMapper _mapper;
        public BlogController(IBlogApiService blogApiService, IMapper mapper)
        {
            _blogApiService = blogApiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await _blogApiService.GetAsync<ResponseModel<List<BlogViewModel>>>();
            return View(blogs.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogModel blogModel)
        {
            if (!ModelState.IsValid)
                return View(blogModel);

            if (await _blogApiService.InsertAsync(blogModel))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, /*WebResponseMessage.CREATED_FAILED*/"");

            return View(blogModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blogViewModel = await _blogApiService.GetAsync<ResponseModel<BlogViewModel>>($"blogs/{id}");

            if (blogViewModel == null)
                return RedirectToAction(nameof(List));

            var blogModel = _mapper.Map<BlogModel>(blogViewModel.Result);

            return View(blogModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogModel blogModel)
        {
            if (!ModelState.IsValid)
                return View(blogModel);

            if (await _blogApiService.UpdateAsync(blogModel))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, /*WebResponseMessage.UPDATED_FAILED*/"");

            return View(blogModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (0 >= id)
                return RedirectToAction(nameof(List));

            await _blogApiService.DeleteAsync($"blogs/Delete/{id}"); ;

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> AssignCategory(int id)
        {
            TempData["blogId"] = id;

            var categories = await _blogApiService.GetAsync<ResponseModel<List<CategoryViewModel>>>("categories");
            var blogCategories = await _blogApiService.GetAsync<ResponseModel<List<CategoryViewModel>>>($"blogs/GetCategories/{id}");

            var assignCategoriesModel = new List<AssignCategoryModel>();

            foreach (var category in categories.Result)
            {
                var model = new AssignCategoryModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    Exists = blogCategories.Result.Any(i => i.Id == category.Id)
                };

                assignCategoriesModel.Add(model);
            }

            return View(assignCategoriesModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> assignCategories)
        {
            int blogId = int.Parse(TempData["blogId"].ToString());

            foreach (var category in assignCategories)
            {
                var model = new CategoryBlogModel
                {
                    BlogId = blogId,
                    CategoryId = category.CategoryId
                };

                if (category.Exists)
                    await _blogApiService.PostAsync("blogs/InsertToCategory", model);
                else
                    await _blogApiService.DeleteAsync($"DeleteToCategory?CategoryId={model.CategoryId}&BlogId={model.BlogId}");
            }

            return RedirectToAction(nameof(AssignCategory), new { id = blogId });
        }
    }
}

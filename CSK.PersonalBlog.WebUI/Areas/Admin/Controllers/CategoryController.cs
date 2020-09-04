using AutoMapper;
using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly IGenericApiService _genericApiService;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _genericApiService.GetAsync<ResponseModel<List<CategoryViewModel>>>("categories");
            return View(categories.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
                return View(categoryModel);
            
            if (await _genericApiService.PostAsync("categories/insert", categoryModel))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, /*WebResponseMessage.CREATED_FAILED*/"");
            return View(categoryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (0 >= id)
                return RedirectToAction(nameof(List));

            var categoryViewModel = await _genericApiService.GetAsync<ResponseModel<CategoryViewModel>>($"categories/{id}");
            var categoryModel = _mapper.Map<CategoryModel>(categoryViewModel.Result);

            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
                return View(categoryModel);

            if (await _genericApiService.PutAsync($"categories/update/{categoryModel.Id}", categoryModel))
                return RedirectToAction(nameof(List));

            ModelState.AddModelError(string.Empty, /*WebResponseMessage.CREATED_FAILED*/"");
            return View(categoryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (0 >= id)
                return RedirectToAction(nameof(List));

            await _genericApiService.DeleteAsync($"categories/delete/{id}");
            return RedirectToAction(nameof(List));
        }
    }
}

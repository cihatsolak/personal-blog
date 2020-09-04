using AutoMapper;
using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Business.Tools.Enums;
using CSK.PersonalBlog.Business.Tools.Facade.Categories;
using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using CSK.PersonalBlog.Entities.Concrete;
using CSK.PersonalBlog.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebApi.Controllers
{
    public class CategoriesController : BaseApiController
    {
        #region Fields
        private readonly ICategoryFacade _categoryFacade;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CategoriesController(ICategoryFacade categoryFacade, IMapper mapper)
        {
            _categoryFacade = categoryFacade;
            _mapper = mapper;
        }
        #endregion

        #region Action Methods

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> All()
        {
            if (_categoryFacade.MemoryCache.TryGetValue("CacheCategoryList", out List<CategoryDto> CacheCategoriesDto))
                return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, CacheCategoriesDto);

            var categories = await _categoryFacade.CategoryService.GetAllSortedByIdAsync();

            if (categories == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            _categoryFacade.MemoryCache.Set("CacheCategoryList", categoriesDto, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30), //30 dakika boyunca cache
                Priority = CacheItemPriority.Low
                //Priority: Önbelleğimiz aşırı derecede doldu ve boşaltılması gerekiyorsa eğer ön bellek boşaltılması sırasında ki önem derecesidir.
            });

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, categoriesDto);
        }

        [HttpGet("{id}"), AllowAnonymous]
        [ServiceFilter(typeof(ValidateIdentifier<Category>))]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryFacade.CategoryService.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, categoryDto);
        }

        [HttpPost("[action]"), ValidateModel]
        public async Task<IActionResult> Insert(CategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            await _categoryFacade.CategoryService.InsertAsync(category);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Created, categoryAddDto);
        }

        [HttpPut("[action]/{id}"), ValidateModel]
        [ServiceFilter(typeof(ValidateIdentifier<Category>))]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.Id)
                return RESPONSE(StatusCodeType.INVALID_ID, StatusMessage.INVALID_ID, ResultType.BadRequest, categoryUpdateDto);

            var updatedCategory = await _categoryFacade.CategoryService.GetByIdAsync(id);
            updatedCategory.Name = categoryUpdateDto.Name;

            await _categoryFacade.CategoryService.UpdateAsync(updatedCategory);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.NoContent);
        }

        [HttpDelete("[action]/{id}")]
        [ServiceFilter(typeof(ValidateIdentifier<Category>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryFacade.CategoryService.DeleteAsync(new Category { Id = id });
            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.NoContent);
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> GetAllWithBlogsCount()
        {
            var categories = await _categoryFacade.CategoryService.GetAllWithCategoryBlogsAsync();

            if (categories == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var listCategoryWithBlogCount = new List<CategoryWithBlogsCountDto>();

            foreach (var category in categories)
            {
                listCategoryWithBlogCount.Add(new CategoryWithBlogsCountDto
                {
                    BlogsCount = category.CategoryBlogs.Count,
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, listCategoryWithBlogCount);
        }
        #endregion
    }
}

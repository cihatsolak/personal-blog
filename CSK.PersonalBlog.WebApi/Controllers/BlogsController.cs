using AutoMapper;
using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Business.Tools.Enums;
using CSK.PersonalBlog.Business.Tools.Facade.Blogs;
using CSK.PersonalBlog.DTO.DTOs.BlogDtos;
using CSK.PersonalBlog.DTO.DTOs.CategoryBlogDtos;
using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using CSK.PersonalBlog.DTO.DTOs.CommentDtos;
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
    public class BlogsController : BaseApiController
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IBlogFacade _blogFacadeService;
        #endregion

        #region Constructor
        public BlogsController(IBlogFacade blogFacadeService, IMapper mapper)
        {
            _blogFacadeService = blogFacadeService;
            _mapper = mapper;
        }
        #endregion

        #region AllowAnonymous Action Methods

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            if (_blogFacadeService.MemoryCache.TryGetValue("CacheBlogList", out List<BlogDto> cacheBlogsDto))
                return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, cacheBlogsDto);

            var blogs = await _blogFacadeService.BlogService.GetAllSortedByPostedTimeAsync();

            if (blogs == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var blogsDto = _mapper.Map<List<BlogDto>>(blogs);

            _blogFacadeService.MemoryCache.Set("CacheBlogList", blogsDto, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5), //5 dakika boyunca cache
                Priority = CacheItemPriority.Normal
                //Priority: Önbelleğimiz aşırı derecede doldu ve boşaltılması gerekiyorsa eğer ön bellek boşaltılması sırasında ki önem derecesidir.
            });

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, blogsDto);
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> GetAllBySearch([FromQuery] string search)
        {
            var blogs = await _blogFacadeService.BlogService.GetAllSearchByPostedTimeAsync(search);

            if (blogs == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound, search);

            var blogsDto = _mapper.Map<List<BlogDto>>(blogs);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, blogsDto);
        }

        [HttpGet("{id}"), AllowAnonymous]
        [ServiceFilter(typeof(ValidateIdentifier<Blog>))]
        public async Task<IActionResult> Get(int id)
        {
            var blog = await _blogFacadeService.BlogService.GetByIdAsync(id);

            var blogDto = _mapper.Map<BlogDto>(blog);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, blogDto);
        }

        [HttpGet("[action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetImageByBlogId(int id)
        {
            var blog = await _blogFacadeService.BlogService.GetByIdAsync(id);

            if (string.IsNullOrEmpty(blog.ImagePath))
                return RESPONSE(StatusCodeType.IMAGE_NOT_FOUND, StatusMessage.IMAGE_NOT_FOUND, ResultType.NotFound, blog);

            return File($"/img/{blog.ImagePath}", MimeType.IMAGE);
        }

        [HttpGet("[action]/{id}"), AllowAnonymous]
        [ServiceFilter(typeof(ValidateIdentifier<Category>))]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            var blogs = await _blogFacadeService.BlogService.GetAllByCategoryIdAsync(id);

            if (blogs == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, blogs);
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> GetLastFiveBlog()
        {
            var blogs = await _blogFacadeService.BlogService.GetLastFiveBlogAsync();

            if (blogs == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var blogsDto = _mapper.Map<List<BlogDto>>(blogs);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, blogsDto);
        }

        [HttpGet("[action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetComments([FromRoute] int id, [FromQuery] int? parentCommentId)
        {
            var comments = await _blogFacadeService.CommentService.GetAllWithSubCommentsAsync(id, parentCommentId);

            if (comments == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var commentsDto = _mapper.Map<List<CommentDto>>(comments);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, commentsDto);
        }

        #endregion

        #region Authorize Action Methods

        [HttpPost("[action]"), ValidateModel]
        public async Task<IActionResult> Insert([FromForm] BlogAddDto blogAddDto)
        {
            var response = await UploadFileAsync(blogAddDto.ImageFile, MimeType.IMAGE);

            if (response.UploadState == UploadState.Error)
                return RESPONSE(StatusCodeType.ERROR, StatusMessage.ERROR, ResultType.BadRequest, response.ErrorMessage);

            if (response.UploadState == UploadState.Success)
                blogAddDto.ImagePath = response.NewName;

            var blog = _mapper.Map<Blog>(blogAddDto);
            await _blogFacadeService.BlogService.InsertAsync(blog);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Created, blogAddDto);
        }

        [HttpPut("[action]/{id}"), ValidateModel]
        [ServiceFilter(typeof(ValidateIdentifier<Blog>))]
        public async Task<IActionResult> Update(int id, [FromForm] BlogUpdateDto blogUpdateDto)
        {
            if (id != blogUpdateDto.Id)
                return RESPONSE(StatusCodeType.INVALID_ID, StatusMessage.INVALID_ID, ResultType.BadRequest, blogUpdateDto);

            var response = await UploadFileAsync(blogUpdateDto.ImageFile, MimeType.IMAGE);

            if (response.UploadState == UploadState.Success)
            {
                blogUpdateDto.ImagePath = response.NewName;
            }
            else if (response.UploadState == UploadState.NotExist)
            {
                var entity = await _blogFacadeService.BlogService.GetByIdAsync(id);
                blogUpdateDto.ImagePath = entity.ImagePath;
            }
            else
                return RESPONSE(StatusCodeType.ERROR, StatusMessage.ERROR, ResultType.BadRequest, response.ErrorMessage);

            var blog = _mapper.Map<Blog>(blogUpdateDto);
            await _blogFacadeService.BlogService.UpdateAsync(blog);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.NoContent);
        }

        [HttpDelete("[action]/{id}")]
        [ServiceFilter(typeof(ValidateIdentifier<Blog>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogFacadeService.BlogService.DeleteAsync(new Blog { Id = id });
            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.NoContent);
        }

        [HttpPost("[action]"), ValidateModel]
        public async Task<IActionResult> InsertToCategory(CategoryBlogDto categoryBlogDto)
        {
            await _blogFacadeService.BlogService.InsertFromCategoryAsync(new CategoryBlog
            {
                BlogId = categoryBlogDto.BlogId,
                CategoryId = categoryBlogDto.CategoryId
            });

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Created, categoryBlogDto);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFromCategory([FromQuery] CategoryBlogDto categoryBlogDto)
        {
            await _blogFacadeService.BlogService.DeleteFromCategoryAsync(new CategoryBlog
            {
                BlogId = categoryBlogDto.BlogId,
                CategoryId = categoryBlogDto.CategoryId
            });

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.NoContent);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertToComment(CommentAddDto commentAddDto)
        {
            var comment = _mapper.Map<Comment>(commentAddDto);

            if (comment == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            await _blogFacadeService.CommentService.InsertAsync(comment);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Created, commentAddDto);
        }

        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidateIdentifier<Blog>))]
        public async Task<IActionResult> GetCategoriesByBlogId(int id)
        {
            var categories = await _blogFacadeService.BlogService.GetCategoriesByBlogIdAsync(id);

            if (categories == null)
                return RESPONSE(StatusCodeType.NOT_FOUND, StatusMessage.NOT_FOUND, ResultType.NotFound);

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return RESPONSE(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, ResultType.Ok, categoryDtos);
        }

        #endregion
    }
}

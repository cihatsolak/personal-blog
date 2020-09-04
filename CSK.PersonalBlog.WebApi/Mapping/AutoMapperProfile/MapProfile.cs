using AutoMapper;
using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using CSK.PersonalBlog.DTO.DTOs.BlogDtos;
using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using CSK.PersonalBlog.DTO.DTOs.CommentDtos;
using CSK.PersonalBlog.Entities.Concrete;

namespace CSK.PersonalBlog.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateBlogDtoMap();
            CreateCategoryDtoMap();
            CreateAppUserDtoMap();
            CreateCommentDtoMap();
        }

        private void CreateBlogDtoMap()
        {
            CreateMap<BlogDto, Blog>();
            CreateMap<Blog, BlogDto>();

            CreateMap<BlogUpdateDto, Blog>();
            CreateMap<Blog, BlogUpdateDto>();

            CreateMap<BlogAddDto, Blog>();
            CreateMap<Blog, BlogAddDto>();
        }

        private void CreateCategoryDtoMap()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
        }

        private void CreateAppUserDtoMap()
        {
            CreateMap<AppUserDto, AppUser>();
            CreateMap<AppUser, AppUserDto>();
        }

        private void CreateCommentDtoMap()
        {
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<CommentAddDto, Comment>();
            CreateMap<Comment, CommentAddDto>();
        }
    }
}

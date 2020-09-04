using AutoMapper;
using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using CSK.PersonalBlog.WebUI.Models.Blogs;
using CSK.PersonalBlog.WebUI.Models.Categories;

namespace CSK.PersonalBlog.WebUI.Tools.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateBlogModelMap();
            CreateCategoryMap();
        }

        public void CreateBlogModelMap()
        {
            CreateMap<BlogViewModel, BlogModel>();
            CreateMap<BlogModel, BlogViewModel>();
        }

        public void CreateCategoryMap()
        {
            CreateMap<CategoryViewModel, CategoryModel>();
            CreateMap<CategoryModel, CategoryViewModel>();
        }
    }
}

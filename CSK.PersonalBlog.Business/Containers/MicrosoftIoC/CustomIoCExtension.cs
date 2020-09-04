using CSK.PersonalBlog.Business.Concrete;
using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.Business.Tools.Facade.Auth;
using CSK.PersonalBlog.Business.Tools.Facade.Blogs;
using CSK.PersonalBlog.Business.Tools.Facade.Categories;
using CSK.PersonalBlog.Business.Tools.JWT;
using CSK.PersonalBlog.Business.ValidationRules.FluentValidation.AppUsers;
using CSK.PersonalBlog.Business.ValidationRules.FluentValidation.Blogs;
using CSK.PersonalBlog.Business.ValidationRules.FluentValidation.CategoryBlogs;
using CSK.PersonalBlog.Business.ValidationRules.FluentValidation.Comments;
using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.DTO.DTOs.AppUserDtos;
using CSK.PersonalBlog.DTO.DTOs.BlogDtos;
using CSK.PersonalBlog.DTO.DTOs.CategoryBlogDtos;
using CSK.PersonalBlog.DTO.DTOs.CategoryDtos;
using CSK.PersonalBlog.DTO.DTOs.CommentDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSK.PersonalBlog.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddExtensionDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonalBlogContext>(options => options.UseSqlServer(configuration.GetConnectionString("db")));

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EfBlogRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();

            services.AddScoped<ICommentDal, EfCommentRepository>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<ILoggerDal, EfLoggerRepository>();
            //services.AddScoped<ILoggerService, LoggerManager>();

            services.AddScoped<IJwtService, JwtManager>();
            
            services.AddScoped<IBlogFacade, BlogFacade>();
            services.AddScoped<IAuthFacade, AuthFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();

            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddDtoValidator>();
            services.AddTransient<IValidator<CategoryBlogDto>, CategoryBlogDtoValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();
            services.AddTransient<IValidator<BlogAddDto>, BlogAddDtoValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddDtoValidator>();

            services.AddMemoryCache(); //Cachleme işlemini gerçekleştirebilirim.
        }
    }
}

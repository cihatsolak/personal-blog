using AutoMapper;
using CSK.PersonalBlog.Business.Concrete;
using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.WebUI.ApiServices.Concerete;
using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using CSK.PersonalBlog.WebUI.Areas.Admin.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSK.PersonalBlog.WebUI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor(); //Bir class'dan httpContext'e eriþebilmek için.
            services.AddSession();

            //services.AddScoped<ILoggerService, LoggerManager>();

            services.AddAutoMapper(typeof(Startup));
            services.AddHttpClient<IBlogApiService, BlogApiManager>();
            services.AddHttpClient<IGenericApiService, GenericApiManager>();
            

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

           

            //Fluent Validator
            services.AddTransient<IValidator<BlogModel>, BlogModelValidator>();
            services.AddTransient<IValidator<CategoryModel>, CategoryModelValidator>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSession();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
                (
                    name: "areas",
                    pattern: "{area:exists}/{controller=Account}/{action=SignIn}/{id?}"
                );

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Account}/{action=SignIn}/{id?}"
                );

            });
        }
    }
}

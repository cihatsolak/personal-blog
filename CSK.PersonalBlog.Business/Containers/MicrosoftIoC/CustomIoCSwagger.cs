using CSK.PersonalBlog.Business.Tools.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CSK.PersonalBlog.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCSwagger
    {
        public static void AddSwaggerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
            var swagger = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swagger.SwaggerDoc.DocName, new OpenApiInfo
                {
                    Title = swagger.SwaggerDoc.Title,
                    Description = swagger.SwaggerDoc.Description,
                    Contact = new OpenApiContact
                    {
                        Email = swagger.SwaggerDoc.Email,
                        Name = swagger.SwaggerDoc.FullName,
                        Url = new Uri(swagger.SwaggerDoc.Url)
                    }
                });

                options.AddSecurityDefinition(swagger.SecurityScheme.Id, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = swagger.SecurityDefinition.Name,
                    Type = SecuritySchemeType.Http,
                    Description = swagger.SecurityDefinition.Description,
                    BearerFormat = swagger.SecurityDefinition.BearerFormat,
                    Scheme = swagger.SecurityDefinition.Scheme
                });

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = swagger.SecurityScheme.Id,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };

                options.AddSecurityRequirement(securityRequirements);
            });
        }
    }
}

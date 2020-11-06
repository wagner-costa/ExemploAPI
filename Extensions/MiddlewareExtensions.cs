using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Exemplo.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Exemplo API",
                    Version = "v3",
                    Description = "API RESTful simples desenvolvida com ASP.NET Core 3.1 para mostrar como criar serviços RESTful usando uma arquitetura desacoplada e sustentável.",
                    Contact = new OpenApiContact
                    {
                        Name = "Wagner Costa",
                        Url = new Uri("https://www.linkedin.com/in/wagner-costa-7ab90782")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // cfg.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Exemplo API");
                options.DocumentTitle = "Exemplo API";
            });
            return app;
        }
    }
}

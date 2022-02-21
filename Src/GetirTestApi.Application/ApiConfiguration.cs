using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using GetirTestApi.Application;
using GetirTestApi.Application.Attributes;

using FluentValidation.AspNetCore;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureApiServices<T>(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(typeof(ApiValidModelStateFilterAttribute));
            })
                .AddApiExplorer()
                .AddFormatterMappings()
                .AddFluentValidation(fv =>
                {
                    // register all validation from current assembly
                    fv.RegisterValidatorsFromAssembly(typeof(ApiConfiguration).Assembly);
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Getir Test API",
                    Description = "An ASP.NET Core Web API Test Project during Interview process for Getir",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = OPENAPI_LICENSE_NAME,
                        Url = new Uri(OPENAPI_LICENSE_URL)
                    },
                    License = new OpenApiLicense
                    {
                        Name = OPENAPI_LICENSE_NAME,
                        Url = new Uri(OPENAPI_LICENSE_URL)
                    }
                });
            });

            services.AddMediatR(typeof(ApiConfiguration).Assembly);
            services.AddScoped<ApiCommandExceptionFilterAttribute>();
            services.AddControllers();
            services.AddInfrastructure(configuration.GetConnectionString(DBCONTEXT_CONNECTIONSTRING_KEY));

            services.AddScoped<IValidationService, ValidationService>();

            return services;
        }

        public static IApplicationBuilder ConfigureApiApplication(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            return app;
        }

        private const string DBCONTEXT_CONNECTIONSTRING_KEY = "OperationsContext";

        private const string OPENAPI_LICENSE_NAME = "Antonello Semeraro";
        private const string OPENAPI_LICENSE_URL = "https://www.linkedin.com/in/antonello-semeraro-849a5115";
    }
}

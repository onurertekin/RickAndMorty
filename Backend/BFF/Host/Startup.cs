using Host.Filters;
using Host.Helpers;
using Host.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Host
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Api

            services.AddSingleton<IHttpResponseFeature, HttpResponseFeature>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<RequiredHeaderParameterFilter>();
                c.CustomSchemaIds((type) => type.FullName.Replace("+", "_"));
                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}{e.HttpMethod}{e.RelativePath}");
            });

            #endregion

            #region Cors

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            #endregion

            #region Registrations

            //...
            services.AddTransient<HttpHelper>();

            #endregion
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors("MyPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();
        }
    }
}
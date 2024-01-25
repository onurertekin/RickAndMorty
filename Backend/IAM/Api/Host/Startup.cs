using DatabaseModel;
using DomainService.Operations;
using Host.Middlewares;
using Microsoft.EntityFrameworkCore;

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

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<RequiredHeaderParameterFilter>();
                c.CustomSchemaIds((type) => type.FullName.Replace("+", "_"));
                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}{e.HttpMethod}{e.RelativePath}");
            });

            #endregion

            #region EntityFramework

            string connectionString = configuration.GetValue<string>("EntityFramework:ConnectionString");
            services.AddDbContext<MainDbContext>(opt => opt.UseSqlServer(connectionString));

            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

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

            services.AddTransient<UserOperation>();
            services.AddTransient<RoleOperation>();
            services.AddTransient<ClaimOperation>();
            services.AddTransient<UserRolesOperation>();
            services.AddTransient<AuthenticationOperation>();


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
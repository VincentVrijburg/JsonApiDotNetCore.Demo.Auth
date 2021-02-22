using JsonApiDotNetCore.Demo.Auth.Api.Extensions;
using JsonApiDotNetCore.Demo.Auth.Api.Services;
using JsonApiDotNetCore.Demo.Auth.Api.Services.Abstractions;
using JsonApiDotNetCore.Demo.Auth.Data;
using JsonApiDotNetCore.Demo.Auth.Data.Repository;
using JsonApiDotNetCore.Demo.Auth.Data.Repository.Abstraction;
using JsonApiDotNetCore.Demo.Auth.Data.Seeds;
using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JsonApiDotNetCore.Demo.Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                    builder.EnableRetryOnFailure(3);
                });
            });

            services.AddScoped<IKeyRepository, KeyRepository>();
            services.AddScoped<IKeyAuthenticationService, KeyAuthenticationService>();
            
            services.AddJsonApi<DataContext>(options =>
            {
                options.Namespace = "api";
            });
            
            services.AddAuthentication("Key").AddKey();
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddFile("logs/log_{Date}.txt");
            }
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseJsonApi();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            // Migrate (and create if not already) the database and populate with test data.
            dataContext.Database.Migrate();
            CarSeed.Initialize(app.ApplicationServices);
        }
    }
}
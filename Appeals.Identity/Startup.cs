using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Appeals.Identity.Data;
using Appeals.Identity.Models;

namespace Appeals.Identity
{
    public class Startup
    {
        public IConfiguration ServiceConfiguration { get; }

        public Startup(IConfiguration configuration) =>
            ServiceConfiguration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = ServiceConfiguration["DbConnection"];

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 5;
              
            })
                .AddEntityFrameworkStores<AuthDbContext>();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/Auth/Login";
            })
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();


            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

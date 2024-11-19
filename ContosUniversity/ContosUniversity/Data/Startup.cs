using ContosUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContosUniversity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the DbContext to use SQL Server with the connection string from configuration
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers with views support
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Set up error handling for different environments
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed errors in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Show a generic error page in production
                app.UseHsts(); // Enforce HTTP Strict Transport Security (HSTS)
            }

            // Add HTTPS redirection middleware
            app.UseHttpsRedirection();

            // Add static files middleware (like CSS, JS, images)
            app.UseStaticFiles();

            // Configure routing
            app.UseRouting();

            // Set up authorization and authentication middleware if needed
            app.UseAuthentication();  // If authentication is enabled in your app
            app.UseAuthorization();   // If authorization policies are in place

            // Define endpoints for controllers and views
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

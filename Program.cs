using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tasks.Data;
using Tasks.Models;


namespace Tasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(builder.Configuration
                .GetConnectionString("ConnectTasksDatabase")))
                .AddIdentity<ApplicationUser, ApplicationRole>(option =>
                {
                    option.Password.RequireDigit = false;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Admin");
                });
                options.AddPolicy("Manager", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
                          || x.User.HasClaim(ClaimTypes.Role, "Admin"));
                });
            });
            
            
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();

            app.Run();

        }
    }
}

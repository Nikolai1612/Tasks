using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tasks.Data;
using Tasks.Entities;
using DotNetEnv;

namespace Tasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(SetDbContextOptions);
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(SetIdentityOPtions)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthorization(SetAuthorizationOptions);

            //builder.Services.AddAuthentication()
            //    .AddFacebook(config =>
            //    {
            //        config.AppId = builder.Configuration["Authentication:Facebook:AppId"];
            //        config.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            //    });

            
            
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();

            app.Run();

            void SetAuthorizationOptions(AuthorizationOptions options)
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
                options.AddPolicy("User", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
                          || x.User.HasClaim(ClaimTypes.Role, "Admin") 
                          || x.User.HasClaim(ClaimTypes.Role,"User"));
                });
            }

            void SetIdentityOPtions(IdentityOptions options)
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }

            void SetDbContextOptions(DbContextOptionsBuilder options)
            {
                Env.Load(".env");
                var connectionstring = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                options.UseSqlServer(connectionstring);
            }
        }
    }
}

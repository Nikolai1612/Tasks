using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tasks.Data;
using Tasks.Entities;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace Tasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(SetDbContextOptions);
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(SetIdentityOPtions)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(SetAuthorizationOptions);

            builder.Services.AddAuthentication()
                .AddFacebook(SetFacebookOptions)
                .AddGoogle(SetGoogleOptions)
                .AddMicrosoftAccount(SetMicrosoftOptions);

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/account/sign-in";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;

            });

            var app = builder.Build();

            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapFallbackToFile("index.html");

            app.Run();


            void SetAuthorizationOptions(AuthorizationOptions options)
            {
                options.AddPolicy("Admin", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Admin");
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
                var connectionstring = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                options.UseSqlServer(connectionstring);
            }

            void SetFacebookOptions(FacebookOptions options)
            {
                options.AppId = Environment.GetEnvironmentVariable("FACEBOOK_APP_ID");
                options.AppSecret = Environment.GetEnvironmentVariable("FACEBOOK_APP_SECRET");
            }

            void SetGoogleOptions(GoogleOptions options)
            {
                options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_APP_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_APP_SECRET");
            }

            void SetMicrosoftOptions(MicrosoftAccountOptions options)
            {
                options.ClientId = Environment.GetEnvironmentVariable("MICROSOFT_APP_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("MICROSOFT_APP_SECRET");
            }
        }

    }
}

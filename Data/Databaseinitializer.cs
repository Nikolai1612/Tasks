using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tasks.Entities;

namespace Tasks.Data
{
    public static class Databaseinitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = "Nikolai1612",
                //LastName = "Dzemchyk",
                //FirstName = "Nikolai"
            };

            var result = userManager.CreateAsync(user,"Nikolai1612").GetAwaiter().GetResult();
            if(result.Succeeded)
            {
                userManager.AddClaimAsync(user,new Claim(ClaimTypes.Role,"Admin"))
                    .GetAwaiter().GetResult();
            }
        }
    }
}

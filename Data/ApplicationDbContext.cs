using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tasks.Entities;

namespace Tasks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override  void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //var user = new ApplicationUser
        //    //{
        //    //    UserName = "User",
        //    //    LastName = "LastName",
        //    //    FirstName = "FirstName"
        //    //};
        //    //var manager = provider.GetService < UserManager<ApplicationUser>>();
        //    //var result = await manager.CreateAsync(user, "123qwe");
        //    //if(result.Succeeded)
        //    //{
        //    //    await manager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
        //    //}
        //}
    }
}

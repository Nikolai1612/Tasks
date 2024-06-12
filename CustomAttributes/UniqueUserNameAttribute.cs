using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Tasks.Data;
using Tasks.Entities;

namespace Tasks.CustomAttributes
{
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var userManager = validationContext.GetService<UserManager<ApplicationUser>>();
                var userName = value as string;
                var user = userManager.FindByNameAsync(userName).GetAwaiter().GetResult();
                if (user != null)
                {
                    return new ValidationResult("Username already exist.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

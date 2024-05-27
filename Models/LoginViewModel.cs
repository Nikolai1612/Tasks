using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public IEnumerable<AuthenticationScheme> ExternalProviders {  get; set; }
    }
}

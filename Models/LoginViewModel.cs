using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class LoginViewModel
    {
        [Required]   
        public string UserName { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Tasks.CustomAttributes;

namespace Tasks.Models
{
    public class SignUpModel
    {
        [Required]
        [UniqueUserName]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}

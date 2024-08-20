using System.ComponentModel.DataAnnotations;
using Tasks.CustomAttributes;

namespace Tasks.Models
{
    public class ExternalSignUpViewModel
    {
        [Required]
        [UniqueUserName]
        public string UserName { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}

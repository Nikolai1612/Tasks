using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}

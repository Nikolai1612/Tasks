using Microsoft.AspNetCore.Identity;

namespace Tasks.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username) { }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

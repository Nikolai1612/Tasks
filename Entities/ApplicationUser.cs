using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser() { }

        public ApplicationUser(string username) : base(username) { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationTask> CreatedTasks { get; set; }
        public ICollection<UserTaskSolution> SolvedTasks { get; set; }

        //public ICollection<Rating> Ratings { get; set; }
    }
}

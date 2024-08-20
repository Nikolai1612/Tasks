using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Entities
{
    public class ApplicationTask
    {
        [Key]
        public int TaskId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public Topic Topic {  get; set; }
        public Guid CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public ICollection<UserTaskSolution> Solvers { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Solution> Solutions { get; set; }

        //public ICollection<Tag> Tags { get; set; }
    }
}

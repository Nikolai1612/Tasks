using Microsoft.EntityFrameworkCore;

namespace Tasks.Entities
{
    public class ApplicationTask
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public Topic Topic {  get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Solution> Solutions { get; set; }
    }
}

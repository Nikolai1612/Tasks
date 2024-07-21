namespace Tasks.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationTask> Tasks { get; set; }
    }
}

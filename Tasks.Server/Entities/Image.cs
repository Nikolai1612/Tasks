namespace Tasks.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
        public int TaskId { get; set; }
        public ApplicationTask Task { get; set; }
    }
}

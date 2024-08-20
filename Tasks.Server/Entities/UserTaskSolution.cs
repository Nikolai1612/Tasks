namespace Tasks.Entities
{
    public class UserTaskSolution
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TaskId { get; set; }
        public ApplicationTask Task { get; set; }
        public int SolutionId { get; set; }
        public Solution Solution { get; set; }
    }
}

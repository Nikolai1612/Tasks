namespace Tasks.Entities
{
    public class Solution
    {
        public int SolutionId { get; set; }
        public string Answer { get; set; }
        public int TaskId { get; set; }
        public ApplicationTask Task { get; set; }
        public ICollection<UserTaskSolution> UserTaskSolutions { get; set; }
    }
}

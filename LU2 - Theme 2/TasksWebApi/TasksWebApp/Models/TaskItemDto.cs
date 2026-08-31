namespace TasksWebApp.Models
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isComplete { get; set; }
        public DateTime? DueDate { get; set; }
    }
}

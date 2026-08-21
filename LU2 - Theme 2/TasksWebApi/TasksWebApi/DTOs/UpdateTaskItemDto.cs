using System.ComponentModel.DataAnnotations;

namespace TasksWebApi.DTOs
{
    public class UpdateTaskItemDto
    {
        [Required(ErrorMessage ="Please fill in the title")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isComplete { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

    }
}

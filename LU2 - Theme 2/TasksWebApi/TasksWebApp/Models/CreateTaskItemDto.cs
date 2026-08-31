using System.ComponentModel.DataAnnotations;

namespace TasksWebApp.Models
{
    public class CreateTaskItemDto
    {

        [Required(ErrorMessage = "Please enter the title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Description too long, shorten it")]
        public string Description { get; set; } = string.Empty;
        public bool isComplete { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}

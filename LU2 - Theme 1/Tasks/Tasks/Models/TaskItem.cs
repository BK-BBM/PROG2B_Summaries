using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="You must enter the Title")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "You must enter the Description")]
        public string Description { get; set; } = "";
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(5);

        public bool isComplete { get; set; } = false;


    }
}

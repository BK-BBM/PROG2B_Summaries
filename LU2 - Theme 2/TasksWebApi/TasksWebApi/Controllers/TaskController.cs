using Microsoft.AspNetCore.Mvc;
using TasksWebApi.Data;
using TasksWebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using TasksWebApi.Entities;

namespace TasksWebApi.Controllers
{
    [ApiController] 
    /*this annotation enables model validation automatically.
     * so, even if we didn't make use of the [Required] annotation in our Entity (TaskItem.cs)
     * it would still validate against empty values
     */
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly DataContext _context;

        /*This constructor here, is an example of Dependency Injection!!!
         We build the DataContext in Program.cs class:
         
        builder.Services.AddDbContext<DataContext>(options =>

        This means a new instance of DataContext is created for every 
        HTTP request in our TaskController class
        
        So, the DataContext uses our connection string in appSettings.json and is passed
        ("injected") directly to our constructor in TaskController.

        The dependency here is the DataContext and if you look at all our TaskController methods
        they make use of it (that's how we're able to read and write to an external DB). 
         */
        public TaskController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var tasks = await _context.Items
            .Select(item => new TaskItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                isComplete = item.isComplete,
                DueDate = item.DueDate
            }).ToListAsync();
            /*as explained, the purpose of ToListAsync is so we can get access to the data without 
             * pausing any ongoing requests (data can flow both ways)
             */

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
           var item = await _context.Items.FindAsync(id);

            if (item == null) {
                return NotFound();//add your message
            }

            var itemDto = new TaskItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                isComplete = item.isComplete,
                DueDate = item.DueDate
            };

            return Ok(itemDto);
            
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>>
            CreateTask(CreateTaskItemDto createTaskDto)
        {
            var item = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                isComplete = false
            };

            _context.Items.Add(item); //we prepare to "INSERT" this into our Items table
            await _context.SaveChangesAsync(); //INSERTS into our Table (the actual DB)

            var output = new TaskItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                isComplete = item.isComplete,
                DueDate = item.DueDate,
            };

            return CreatedAtAction(nameof(CreateTask),new {Id = item.Id},output);
            /*this returns HTTP 201 = created. if there are problems
             * the program will throw an error before this line is executed
             * this line only gets executed if task creation was a success
             */ 
        }
    }
}

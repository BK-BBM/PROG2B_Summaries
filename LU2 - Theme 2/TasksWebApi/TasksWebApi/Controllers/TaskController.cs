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


        /*26/08/2026: UserAuth using Session Management
         "Session Variables" 
         SessionKey to store session data on the server
         and Cookie to store preferences on the user's computer /browser.

         We're able to do this primarily because of 

         builder.Services.AddDistributedMemoryCache();  in Program.cs
         */
        private const string AuthSessionKey = "UserSession";
        private const string AuthCookie = "UserCookie";

        

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

        
        
        //Login methods : 26/08/2026


        /* Class Exercise: 26/08/2026
         * 
         * Add a secondary user, that is also able to login,
         * View and manage tasks except for deleting.
         * 
         */

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            /*To login we normally get our username and password combo
             * from the DB and pass that info via the LoginDto 
             * (Values hardcoded for demo + we didn't have time
             * to create another DBSet and link User to TaskItems)
             
               
               After getting the login details, we store them in our session variables
               using HttpContext (HttpContext is responsible for storing session data
               as well as sending info between the server and client)

               Server = our Web API
               Client = Wep app on a user's browser
               
               The HttpContext.Response. is what our Web API sends back to the client.
               The client will send HttpContext.Request (if it wants something) 
              
               Then we set the cookie options.
               
               
             */
            if (loginDto.Username == "admin" && loginDto.Password == "Admin123"
                //the line below is part of my class excercise attempt.
                || loginDto.Username == "Taelo" && loginDto.Password == "Admin123")
            {
                HttpContext.Session.SetString(AuthSessionKey, loginDto.Username);

                HttpContext.Response.Cookies.Append(AuthCookie, loginDto.Username,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });

                return Ok(new { Message = "Login successful"+
                
                    "Welcome back " + loginDto.Username
                });
            }

            return Unauthorized("Invalid credentials");
        }
 
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            /* We delete all session variables.
               From the server and client.
             */
            HttpContext.Session.Clear();

            HttpContext.Response.Cookies.Delete(AuthCookie);

            return Ok(new { Message = "Logged out successfully" });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var sessionUser = HttpContext.Session.GetString(AuthSessionKey);
            var userCookie = HttpContext.Request.Cookies[AuthCookie];

            if (string.IsNullOrEmpty(sessionUser) || string.IsNullOrEmpty(userCookie))
            {
                return Unauthorized("Please login first");
            }

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
            /*check if user's logged in. This logic is repeated across our Http endpoints.
             * The AuthSessionKey will store the username (either admin or Taelo in my case)
             */

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(AuthSessionKey)))
            {
                return Unauthorized("You need to login first");
            }

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(AuthSessionKey)))
            {
                return Unauthorized("You need to login first");
            }

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

            return CreatedAtAction(nameof(CreateTask), new { Id = item.Id }, output);
            /*this returns HTTP 201 = created. if there are problems
             * the program will throw an error before this line is executed
             * this line only gets executed if task creation was a success
             */
        }

        //Exercises to try on your own

        //1. Update method

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskItemDto updateTask)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(AuthSessionKey)))
            {
                return Unauthorized("You need to login first");
            }

            //type method logic  | Try it yourself exercise
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound("Item is not found");
            }

            item.Title = updateTask.Title;
            item.Description = updateTask.Description;
            item.isComplete = updateTask.isComplete;
            item.DueDate = updateTask.DueDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        //2. Delete method

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            /* Normal logic to check if user is signed in (before class exercise)
             * 
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(AuthSessionKey)))
            {
                return Unauthorized("You need to login first");
            }
            
             */

            /*class exercise attempt 
             * 
             * refactored the code so I can check if the logged in user is admin or not.
             * if not admin, then prevent deleting.
             * 
             * There are a few ways to attempt this, some people still used 
             *  Unathorized -> throwing a 401.
             *  
             *  I wanted to throw a 403 (Forbidden) because our user is 
             *  authorized to use the app, they're just not allowed (Forbidden) to delete
             *  because they don't have permissions.
             */
            string? currentUser = HttpContext.Session.GetString(AuthSessionKey);
            if (string.IsNullOrEmpty(currentUser))
            {
                return Unauthorized("You need to login first");
            }

            //prevent normal user from deleting 
            if (currentUser != "admin")
            {
                //return Forbid("Only admins can delete tasks");
                /*return Forbid was giving me an HTTP500 error, because
                 * I'm not using the built-in Authentication
                 
                 Since we're using IActionResult and we can set different 
                 reponse and status codes, I opted for setting a StatusCode instead.
                 */
                return StatusCode(403, "Only admins can delete tasks");
            }
         
            //type method logic | Try it yourself exercise.
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }
            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

           return NoContent();
            
        } 

    }
}

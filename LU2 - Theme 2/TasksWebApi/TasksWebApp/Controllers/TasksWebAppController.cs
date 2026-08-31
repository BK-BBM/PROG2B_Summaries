using Microsoft.AspNetCore.Mvc;
using TasksWebApp.Models;

namespace TasksWebApp.Controllers
{
    public class TasksWebAppController : Controller
    {
        private readonly HttpClient httpClient;

        public TasksWebAppController(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("TasksWebApi");
        }


        /*
          Remember, the Action methods that return views 
          return the actual views (the razor pages).
          
          This is the same as writing an HttpGet (but we just get the view) 
         */
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var response = await httpClient.PostAsJsonAsync("api/task/login", login);

            if (response.IsSuccessStatusCode) //if response = Ok()
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid username and password combination");
            return View(login);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var response = await httpClient.PostAsJsonAsync("api/task/logout", new { });

            return RedirectToAction(nameof(Login));
        }

        //a method to get all our Tasks which for this example will be our "index".

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var response = await httpClient.GetAsync("api/task");

            if (!response.IsSuccessStatusCode)
            {
                /* this will check if the person viewing the tasks is logged in or not.
                 * If  they're not logged in, redirect them to the login page.
                 */

                return RedirectToAction(nameof(Login));
            }

            var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItemDto>>();

            return View(tasks);
        }

        //Create Tasks HttpGet (for the view) + HttpPost (for the logic) 

        public IActionResult Create()
        {
            return View(new CreateTaskItemDto { DueDate = DateTime.Today.AddDays(5) });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskItemDto createTaskItemDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createTaskItemDto);
            }

            var response = await httpClient.PostAsJsonAsync("api/task", createTaskItemDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to save task");

            return View(createTaskItemDto);
        }

        /*
          PROG6212 Group 2: ICE Task 02.
          
          Add Logic to Edit and delete on the MVC App.
          
          Due date: 07 September 23H59.
         
         */
    }
}

using Microsoft.AspNetCore.Mvc;

using Tasks.Models;
namespace Tasks.Controllers
{
    public class TaskController : Controller
    {
        private static readonly List<TaskItem> tasks = new() {

            new TaskItem{Id=1,Title="Practice CLDV", Description="Learn MVC",isComplete=true},
            new TaskItem{Id=2,Title="Submit PROG POE", Description="Make final submission",isComplete=false},
            new TaskItem{Id=3,Title="Study", Description="Practice all modules",isComplete=false}

            /* We create an "in-memory" DB using a generic List. 
             * Obvisouly the list will be of type TaskItem because that's what we're
             * interested in (for this example) 
             */
        };
        public IActionResult Index()
        {
            return View(tasks);
            /* As mentioned in class, using the interface IActionResult
            * implies the methods we are creating will do something (action methods)
            * 
            * From this point, I will refer to these as "action methods"
            * 
            * 
            * This is essentially an [HttpGet] method.
            * We are telling the controller, that "return the view to us" 
            * 
            * Action Methods are also useful because they "bind" the controller
            * to the view especially where we use <form> tags and have 
            * to specify actions (what must happen when the submit button is clicked)
            * NB- remember this as it is the basis of displaying different views
            * using controller methods
            */
        }



        /* This action method below toggles the status of a task between
         * complete and incomplete it is an HttPost method meaning it 
         * changes data at the source. In this example, that's our list.
         * 
         */
        [HttpPost]
        public IActionResult ToggleStatus(int id) //we take an id of the task we'll change as a parameter
        { 
            var task = tasks.FirstOrDefault(t  => t.Id == id);
            /* this is LINQ (Select * From Table Where Id = Id(this is the parameter we specificied) 
             * but since we don't actually pass a parameter, we want the id to be equal to 
             * the Id of the selected item.
             * 
             * FirstOrDefault() just means -> stop searching once you find the item
             * (this will become clearer as we progress) 
             */

            if (task != null) {
                task.isComplete = !task.isComplete;
            }

            return RedirectToAction("Index");
            /*once we're done, load the default view "Index" (Tasks > Index)
             * meaning we're forcing the browser to reload (just a GET request) 
             * 
             * Unrelated but valuable -> this is HTTP code 302!
             * 
             * remember the "proper" usage is return RedirectToAction(nameof(Index));
             * the program will still convert it to return RedirectToAction("Index");
             * 
             */

        }


        //create tasks


        public IActionResult Create() {

            return View(new TaskItem());

            /*this action method just loads the create task view (Tasks > Create.cshtml)
             * 
             */
        }


        /* this is the action method that actually creates a task (Post = changes will be made
         * if it runs successfully)
         * 
         * please note that all the Get and Post methods use a similar shape. 
         * So, I'll just highlight the main differences
         */
        [HttpPost]
        public IActionResult Create(TaskItem newTask) {

            if (!ModelState.IsValid) {

                return View(newTask);
                /* this is where we catch any errors and display them to the user 
                 * .NET does the bulk of the work here in checking ModelState validity.
                 *Using annotations like [Required] also helps in this regard
                 */
            }

            newTask.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            /* this line allows us to autoincrement a new record.
             * In MySQL it is the same as using : int id autoincrement
             * In SQL: int id Identity(1,1)
             * 
             * We just check if there are any tasks in our list.
             * If there is, we get the "highest" id (tasks.Max(t=> t.Id))
             * Once we get the highest id, we just add +1 to it.
             * There's a fallback condition| (we add +1 here) :1 (this is our fallback)
             * the fallback is, if there are no tasks at all in our list, then
             * make 1 the id of the task we're going to add.
             * 
             */
            tasks.Add(newTask); //add the task to the list
            return RedirectToAction("Index"); //force redirect

        }

        public IActionResult Edit(int id)
        { 

            var taskItem = tasks.FirstOrDefault(t =>t.Id == id);

            if (taskItem == null) {
                return NotFound();
            }

            return View(taskItem);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskItem taskItem) 
        {
            if (id != taskItem.Id) {
                return NotFound(); //get 404 if we don't find our task
            }

            if (!ModelState.IsValid) {
                return View(taskItem);
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) {
                return NotFound();
            }

            /* this is a SQL update statement 
             * Update Task
             * 
             * SET Title = "New title", Description = "New description", DueDate = "New duedate"
             * 
             * Where Id = the id we take as a parameter. 
             */
            task.Title = taskItem.Title;
            task.Description = taskItem.Description;
            task.DueDate = taskItem.DueDate;
            //task.isComplete = taskItem.isComplete;//set to false by default 

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var taskItem = tasks.FirstOrDefault(t=> t.Id == id);
            if (taskItem == null) {
                return NotFound();
            }

            return View(taskItem);
        }


        /* forgot to mention this in class
         * the reason I wanted to add an ActionName here is 
         * just in case you wanted to create a delete method that 
         * has characters .NET wouldn't allow in an identifier (at least this used to be the case long ago)
         * otherwise, the HttPost would suffice
         */
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var taskItem = tasks.FirstOrDefault(t => t.Id == id);

            if (taskItem == null)
            {
                return NotFound();
            }

            tasks.Remove(taskItem); 
            /*after getting the id of the task
             * we just remove it from our list.
             * the Remove() method is from the List<> class
             */

            return RedirectToAction("Index");
        }
        
       
    }
}

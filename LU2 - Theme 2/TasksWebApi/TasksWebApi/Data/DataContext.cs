using Microsoft.EntityFrameworkCore;
using TasksWebApi.Entities;

namespace TasksWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {

        }
        
        /* Table mapping.
         * We want to use TaskItem/ a "collection" of TaskItem and 
         * make them into a Table called Items in our DB
         
         */
        public DbSet<TaskItem> Items { get; set; }
    }
}

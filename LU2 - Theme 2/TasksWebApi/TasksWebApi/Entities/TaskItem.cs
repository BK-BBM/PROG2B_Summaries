namespace TasksWebApi.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isComplete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

    /* As explained in class, we're calling this our "Entity" because we're going to create 
     * a table from this class using DBSet
     * 
     * All these attributes will from Table columns in SQL 
     */
}

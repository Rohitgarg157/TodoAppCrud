
namespace MyTodoApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // High, Medium, Low
        public string Category { get; set; } // Work, Personal
        public DateTime CreatedDate { get; set; }

       
    }
}

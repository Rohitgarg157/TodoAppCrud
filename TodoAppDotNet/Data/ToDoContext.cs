using Microsoft.EntityFrameworkCore;
using MyTodoApp.Models;

namespace MyTodoApp.Data
{
    public class ToDoContext : DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using MyTodoApp.Data;
using MyTodoApp.Models;

namespace MyTodoApp.Repositories
{
    public class ToDoRepository : IToDoRepository
    {


        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
            try
            {



                var item = await _context.ToDoItems.FindAsync(id);

            Console.WriteLine("Id Found");
            if (item == null)
            {
                Console.WriteLine("No Id Found ");
                    //  throw new Exception("No data id");
                    throw new KeyNotFoundException($"No ToDoItem found with ID {id}");

                    return null;
            }
            
                return await _context.ToDoItems.FindAsync(id);

            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine($"Repository error: {ex.Message}");
                throw;
            }
        }


        

        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> DeleteAsync(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null) return null;

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> SearchAsync(string query)
        {
            //  return await _context.ToDoItems
            //    .Where(item => item.Title.Contains(query) || item.Description.Contains(query))
            //  .ToListAsync();


            try
            {
                var result= await _context.ToDoItems
                .Where(item => item.Title.Contains(query) || item.Description.Contains(query))
                .ToListAsync();
                if (result == null || !result.Any())
                {
                    Console.WriteLine("No items found matching the query.");
                    return new List<ToDoItem>();
                }
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for items: {ex.Message}");


                throw;
            }

        }
    }
}

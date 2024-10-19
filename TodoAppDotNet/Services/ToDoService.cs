using MyTodoApp.Models;
using MyTodoApp.Repositories;

namespace MyTodoApp.Services
{
    public class ToDoService : IToDoService
    {

        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Service error: {ex.Message}");
                throw;
            }
        }


        



        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            return await _repository.UpdateAsync(item);
        }

        public async Task<ToDoItem> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ToDoItem>> SearchAsync(string query)
        {
   
            try
            {
                return await _repository.SearchAsync(query);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}

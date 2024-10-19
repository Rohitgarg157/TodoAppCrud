﻿using MyTodoApp.Models;

namespace MyTodoApp.Services
{
    public interface IToDoService
    {

        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem> GetByIdAsync(int id);


        Task<ToDoItem> AddAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(ToDoItem item);
        Task<ToDoItem> DeleteAsync(int id);
        Task<IEnumerable<ToDoItem>> SearchAsync(string query);
    }
}

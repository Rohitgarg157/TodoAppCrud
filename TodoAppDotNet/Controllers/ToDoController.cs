using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Models;
using MyTodoApp.Services;

namespace MyTodoApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Get()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> Get(int id)
        {
            //var item = await _service.GetByIdAsync(id);
           // if (item == null) return NotFound();
           // return Ok(item);
           
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException knfEx)
            {
                // Return a 404 Not Found response
                return NotFound(new { Message = knfEx.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }




        }


      



        [HttpPost]
        public async Task<ActionResult<ToDoItem>> Post(ToDoItem item)
        {
            var createdItem = await _service.AddAsync(item);

            Console.WriteLine("Its work");
            return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ToDoItem item)
        {
            if (id != item.Id) return BadRequest();

            var updatedItem = await _service.UpdateAsync(item);
            if (updatedItem == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.DeleteAsync(id);
            if (item == null) return NotFound();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Search([FromQuery] string query)
        {
            // var items = await _service.SearchAsync(query);
            // return Ok(items);

            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter cannot be null or empty.");
            }

            try
            {
                var items = await _service.SearchAsync(query);

                if (items == null || !items.Any())
                {
                    throw new Exception("No Item Found");
                    return NotFound("No items found matching the query.");
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,  new { Message = "An error occurred while searching for items.", Details = ex.Message });
            }

        }


    }
}

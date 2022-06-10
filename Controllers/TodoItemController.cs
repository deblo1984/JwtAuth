using JwtAuth.Context;
using JwtAuth.Entities;
using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        public TodoItemController(IUserService userService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();
            return Ok(todoItem);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {

            return await _dbContext.TodoItems.Select(x => ItemToDto(x)).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(int id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return ItemToDto(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            todoItem.IsComplete = todoItemDto.IsComplete;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemsExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _dbContext.TodoItems.Remove(todoItem);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        private bool TodoItemsExists(int id)
        {
            return _dbContext.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDto ItemToDto(TodoItem todoItem) => new TodoItemDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}
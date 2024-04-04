using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoApp.Data;
using todoApp.Models;

namespace todoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly dataContext _context;

        public TodoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoText>>> GettodoTexts()
        {
          if (_context.todoTexts == null)
          {
              return NotFound();
          }
            return await _context.todoTexts.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoText>> GetTodoText(int id)
        {
          if (_context.todoTexts == null)
          {
              return NotFound();
          }
            var todoText = await _context.todoTexts.FindAsync(id);

            if (todoText == null)
            {
                return NotFound();
            }

            return todoText;
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoText updatedTodo)
        {
            try
            {
                var todoExists = await _context.todoTexts.AnyAsync(x => x.Id == id);

                if (!todoExists)
                {
                    return NotFound();
                }

                var existingTodo = await _context.todoTexts.FindAsync(id);

                existingTodo.Title = updatedTodo.Title;
                existingTodo.isCompleted = updatedTodo.isCompleted;
                existingTodo.UpdateTime = updatedTodo.UpdateTime;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

            // POST: api/Todo
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
        public async Task<ActionResult<TodoText>> PostTodoText(TodoText todoText)
        {
          if (_context.todoTexts == null)
          {
              return Problem("Entity set 'dataContext.todoTexts'  is null.");
          }
            
            todoText.CreatedDate = DateTime.Now;
            
            _context.todoTexts.Add(todoText);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoText", new { id = todoText.Id }, todoText);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoText(int id)
        {
            if (_context.todoTexts == null)
            {
                return NotFound();
            }
            var todoText = await _context.todoTexts.FindAsync(id);
            if (todoText == null)
            {
                return NotFound();
            }

            _context.todoTexts.Remove(todoText);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoTextExists(int id)
        {
            return (_context.todoTexts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

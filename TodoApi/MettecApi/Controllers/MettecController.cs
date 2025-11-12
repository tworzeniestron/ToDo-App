using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MettecApi.Data;
using MettecApi.Models;

namespace MettecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MettecController : ControllerBase
    {
        private readonly MettecContext _context;

        public MettecController(MettecContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MettecItem>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MettecItem>> CreateTodo(MettecItem todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoStatus(int id, MettecItem updated)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            todo.IsDone = updated.IsDone;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

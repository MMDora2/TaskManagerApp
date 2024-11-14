using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTask(TaskItem taskItem)
        {
            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}

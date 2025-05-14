using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Authorize(Roles = "User,Admin")] // Requires any authenticated user
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public TasksController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [Authorize(Roles = "Admin")] // Only admins can create tasks
        // POST api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            task.CreatedAt = DateTime.UtcNow;
            task.IsCompleted = false;

            var id = await _dbService.CreateTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id }, task);
        }

        [Authorize(Roles = "User,Admin")] // Both roles can get tasks
        // GET api/tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _dbService.GetTaskById(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // GET api/tasks/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _dbService.GetTasksByUserId(userId);
            return Ok(tasks);
        }
    }
}
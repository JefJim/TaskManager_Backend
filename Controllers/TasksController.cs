using Microsoft.AspNetCore.Mvc;
using TaskManager_Backend.Models;
using TaskManager_Backend.Services;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _taskService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItem task)
    {
        await _taskService.CreateAsync(task);
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TaskItem updatedTask)
    {
        var success = await _taskService.UpdateAsync(id, updatedTask);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _taskService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}

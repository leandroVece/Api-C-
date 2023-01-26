using API.Service;
using EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService TaskService)
    {
        _taskService = TaskService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_taskService.Get());
    }
    [HttpPost]
    public IActionResult Post([FromBody] EF.Models.Task Task)
    {
        _taskService.Post(Task);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] EF.Models.Task Task)
    {
        _taskService.update(id, Task);
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _taskService.Delete(id);
        return Ok();
    }

}
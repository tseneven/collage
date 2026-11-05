using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pract7.Application.Services;

namespace pract7.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TasksController(TaskService taskService) : ControllerBase
{
    [HttpPut("{id:int}")]
    [Authorize]
    public IActionResult UpdateTask(int id, [FromBody] UpdateTaskRequest request)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        var task = taskService.UpdateTask(id, request.ToModel(), User.Identity.Name, User.IsInRole("Admin"));
        if (task.Result == GetResult.NotFound)
            return NotFound();
        if (task.Result == GetResult.NoAccess)
            return Forbid();
        if (task.Result == GetResult.Error)
            return StatusCode(500);
        return Ok(TaskResponse.FromModel(task.Task!));
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public IActionResult DeleteTask(int id)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        var result = taskService.DeleteTask(id, User.Identity.Name, User.IsInRole("Admin"));
        if (result == GetResult.NotFound)
            return NotFound();
        if (result == GetResult.NoAccess)
            return Forbid();
        if (result == GetResult.Error)
            return StatusCode(500);
        return NoContent();
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllTasks()
    {
        var result = taskService.GetAllTasks();
        if (result.Result == GetResult.Error)
            return StatusCode(500);

        return Ok(result.Task!.Select(TaskResponse.FromModel).ToArray());
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        var task = taskService.GetTasks(User.Identity.Name);
        if (task.Result == GetResult.Error)
            return StatusCode(500);
        return Ok(task.Task!.Select(TaskResponse.FromModel).ToArray());
    }


    [HttpGet("{id:int}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();

        var task = taskService.GetTask(id, User.Identity.Name, User.IsInRole("Admin"));
        if (task.Result == GetResult.NotFound)
            return NotFound();
        if (task.Result == GetResult.NoAccess)
            return Forbid();
        if (task.Result == GetResult.Error)
            return StatusCode(500);
        return Ok(TaskResponse.FromModel(task.Task!));
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateTask([FromBody] CreateTaskRequest request)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        var createResult = taskService.CreateTask(request.ToModel(User.Identity.Name));
        if(createResult.Result == CreateResult.Error)
            return StatusCode(500);
        return CreatedAtAction(nameof(Get), new { id = createResult.Task!.Id },
            TaskResponse.FromModel(createResult.Task));
    }
}

public class UpdateTaskRequest()
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsCompleted { get; set; }

    public UpdateTaskModel ToModel() => new()
    {
        Title = Title,
        Description = Description,
        IsCompleted = IsCompleted
    };
}

public class TaskResponse()
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string OwnerLogin { get; set; } = null!;
    public bool IsCompleted { get; set; }

    public static TaskResponse FromModel(pract7.Application.Models.Task task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        OwnerLogin = task.OwnerLogin,
        IsCompleted = task.IsCompleted
    };
}

public class CreateTaskRequest()
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public CreateTaskModel ToModel(string ownerLogin) => new()
    {
        Title = Title,
        Description = Description,
        OwnerLogin = ownerLogin
    };  
}

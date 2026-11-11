using pract7.Application.DBContext;
using Task = pract7.Application.Models.Task;

namespace pract7.Application.Services;

public class TaskService(Trpo7Context context)
{
    public GetTaskResult UpdateTask(int id, UpdateTaskModel model, string ownerLogin, bool isAdmin)
    {
        var result = GetTask(id, ownerLogin, isAdmin);
        if (result.Result != GetResult.Success)
            return result;

        try
        {
            var task = result.Task!;
            task.Title = model.Title;
            task.Description = model.Description;
            task.IsCompleted = model.IsCompleted;
            context.SaveChanges();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new GetTaskResult
            {
                Task = null,
                Result = GetResult.Error
            };
        }
    }

    public GetResult DeleteTask(int id, string ownerLogin, bool isAdmin)
    {
        var result = GetTask(id, ownerLogin, isAdmin);
        if (result.Result != GetResult.Success)
            return result.Result;

        try
        {
            context.Tasks.Remove(result.Task!);
            context.SaveChanges();
            return GetResult.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return GetResult.Error;
        }
    }

    public GetTasksResult GetAllTasks()
    {
        try
        {
            var task = context.Tasks.ToArray();
            return new GetTasksResult
            {
                Task = task,
                Result = GetResult.Success
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new GetTasksResult
            {
                Task = Array.Empty<Task>(),
                Result = GetResult.Error
            };
        }
    }

    public GetTaskResult GetTask(int id, string ownerLogin, bool isAdmin)
    {
        try
        {
            var task = context.Tasks.SingleOrDefault(x => x.Id == id);
            if (task == null)
                return new GetTaskResult
                {
                    Task = null,
                    Result = GetResult.NotFound
                };
            if (task.OwnerLogin != ownerLogin
                && !isAdmin)
                return new GetTaskResult
                {
                    Task = null,
                    Result = GetResult.NoAccess
                };

            return new GetTaskResult
            {
                Task = task,
                Result = GetResult.Success
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new GetTaskResult
            {
                Task = null,
                Result = GetResult.Error
            };
        }
    }

    public GetTasksResult GetTasks(string ownerLogin)
    {
        try
        {
            var task = context.Tasks.Where(x => x.OwnerLogin == ownerLogin).ToArray();

            return new GetTasksResult
            {
                Task = task,
                Result = GetResult.Success
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new GetTasksResult
            {
                Task = Array.Empty<Task>(),
                Result = GetResult.Error
            };
        }
    }

    public CreateTaskResult CreateTask(CreateTaskModel model)
    {
        try
        {
            var task = new Task
            {
                Title = model.Title,
                Description = model.Description,
                OwnerLogin = model.OwnerLogin,
                IsCompleted = false,
            };
            context.Tasks.Add(task);
            context.SaveChanges();
            return new CreateTaskResult
            {
                Task = task,
                Result = CreateResult.Success
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new CreateTaskResult
            {
                Task = null,
                Result = CreateResult.Error
            };
        }
    }
}

public class GetTaskResult()
{
    public Task? Task { get; set; }
    public GetResult Result { get; set; }
}

public class CreateTaskResult()
{
    public Task? Task { get; set; }
    public CreateResult Result { get; set; }
}

public class GetTasksResult()
{
    public Task[]? Task { get; set; }
    public GetResult Result { get; set; }
}

public enum GetResult
{
    NotFound,
    NoAccess,
    Success,
    Error
}

public enum CreateResult
{
    Error,
    Success,
}

public class CreateTaskModel()
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string OwnerLogin { get; set; }
}

public class UpdateTaskModel()
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
}

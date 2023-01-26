using EF.Models;

namespace API.Service;
public class TaskService : ITaskService
{
    TaskContext context;

    public TaskService(TaskContext dbcontext)
    {
        context = dbcontext;
    }

    public IEnumerable<EF.Models.Task> Get()
    {
        return context.Tasks;
    }

    //Esta es la razon por la que no debemos usar palabras recervadas.
    public async System.Threading.Tasks.Task Post(EF.Models.Task Task)
    {
        context.Add(Task);
        await context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task update(Guid id, EF.Models.Task Task)
    {
        var TaskPresent = context.Tasks.Find(id);
        if (TaskPresent != null)
        {
            TaskPresent.Title = Task.Title;
            TaskPresent.Description = Task.Description;
            TaskPresent.Date = Task.Date;
            TaskPresent.PriorityTask = Task.PriorityTask;

            await context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task Delete(Guid id)
    {
        var TaskPresent = context.Categoties.Find(id);
        if (TaskPresent != null)
        {
            context.Remove(TaskPresent);
            await context.SaveChangesAsync();
        }
    }

}

public interface ITaskService
{
    IEnumerable<EF.Models.Task> Get();
    System.Threading.Tasks.Task Delete(Guid id);
    System.Threading.Tasks.Task update(Guid id, EF.Models.Task Task);
    System.Threading.Tasks.Task Post(EF.Models.Task Task);

}
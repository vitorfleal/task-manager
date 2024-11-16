using TaskManager.Application.Features.TaskJobs;

namespace TaskManager.Infrastructure.Contexts;

public class TaskManagerDatabaseSeed
{
    private readonly TaskManagerContext _context;

    public TaskManagerDatabaseSeed(TaskManagerContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        SeedTaskJob();
    }


    private void SeedTaskJob()
    {
        if (!_context.TaskJobs.Any())
        { 
            var taskJob = new TaskJob("Task Job test 1", "Task Job test description 1", DateTime.UtcNow, 1);

            _context.TaskJobs.AddRange(taskJob);
            _context.SaveChanges();
        }
    }
}
using TaskManager.Application.Features.Status;
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
        SeedStatus();
        SeedTaskJob();
    }

    private void SeedStatus()
    {
        if (!_context.Status.Any())
        {
            var statusCollection = new List<Status>
            {
                new("Status test 1"),
                new("Status test 1"),
                new("Status test 1")
            };

            _context.Status.AddRange(statusCollection);
            _context.SaveChanges();
        }
    }

    private void SeedTaskJob()
    {
        if (!_context.TaskJobs.Any() && _context.Status.Any())
        {
            var status = _context.Status.First();

            var taskJob = new TaskJob("Task Job test 1", "Task Job test description 1", DateTime.UtcNow, 1, status.Id);

            _context.TaskJobs.AddRange(taskJob);
            _context.SaveChanges();
        }
    }
}
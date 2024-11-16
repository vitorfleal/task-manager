using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Features.TaskJobs;
using TaskManager.Application.Features.TaskJobs.Repositories;
using TaskManager.Infrastructure.Contexts;

namespace TaskManager.Infrastructure.Repositories;

public class TaskJobRepository : ITaskJobRepository
{
    private readonly DbSet<TaskJob> _tasks;

    public TaskJobRepository(TaskManagerContext context)
    {
        _tasks = context.TaskJobs;
    }

    public async Task AddAsync(TaskJob taskJob)
    {
        await _tasks.AddAsync(taskJob);
    }

    public async Task<IEnumerable<TaskJob>> GetAllAsync() => await _tasks.AsNoTracking().ToListAsync();

    public async Task<TaskJob?> GetByIdAsync(Guid id) => await _tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public void Remove(TaskJob taskJob)
    {
        _tasks.Remove(taskJob);
    }

    public void Update(TaskJob taskJob)
    {
        _tasks.Update(taskJob);
    }
}
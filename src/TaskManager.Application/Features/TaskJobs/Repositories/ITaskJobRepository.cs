namespace TaskManager.Application.Features.TaskJobs.Repositories;

public interface ITaskJobRepository
{
    Task AddAsync(TaskJob taskJob);

    Task<TaskJob?> GetByIdAsync(Guid id);

    Task<IEnumerable<TaskJob>> GetAllAsync();

    void Update(TaskJob taskJob);

    void Remove(TaskJob taskJob);
}
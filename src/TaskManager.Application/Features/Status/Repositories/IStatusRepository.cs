namespace TaskManager.Application.Features.Status.Repositories;

public interface IStatusRepository
{
    Task<Status?> GetByIdAsync(Guid id);

    Task<IEnumerable<Status>> GetAllAsync();
}
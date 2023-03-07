using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Features.Status;
using TaskManager.Application.Features.Status.Repositories;
using TaskManager.Infrastructure.Contexts;

namespace TaskManager.Infrastructure.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly DbSet<Status> _status;

    public StatusRepository(TaskManagerContext context)
    {
        _status = context.Status;
    }

    public async Task<IEnumerable<Status>> GetAllAsync() => await _status.AsNoTracking().ToListAsync();

    public async Task<Status?> GetByIdAsync(Guid id) => await _status.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
using TaskManager.Application.Base.Persistence;

namespace TaskManager.Infrastructure.Contexts.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskManagerContext _context;

    public UnitOfWork(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
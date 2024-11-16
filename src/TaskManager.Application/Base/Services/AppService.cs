using TaskManager.Application.Base.Persistence;

namespace TaskManager.Application.Base.Services;

public abstract class AppService
{
    private readonly IUnitOfWork _unitOfWork;

    protected AppService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected async Task Commit()
    {
        await _unitOfWork.Commit();
    }
}
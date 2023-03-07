namespace TaskManager.Application.Base.Persistence;

public interface IUnitOfWork
{
    Task Commit();
}
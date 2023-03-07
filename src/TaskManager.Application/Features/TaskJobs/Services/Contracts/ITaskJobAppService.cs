using TaskManager.Application.Base.Models;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Responses;

namespace TaskManager.Application.Features.TaskJobs.Services.Contracts;

public interface ITaskJobAppService
{
    Task<(Response, TaskJob?)> CreateTaskJob(CreateTaskJobRequest request, CancellationToken cancellationToken = default);

    Task<(Response, TaskJob?)> UpdateTaskJob(UpdateTaskJobRequest request, CancellationToken cancellationToken = default);

    Task<Response> RemoveTaskJob(Guid id, CancellationToken cancellationToken = default);

    Task<(Response, TaskJobResponse?)> GetTaskJobById(Guid id);

    Task<(Response, IEnumerable<TaskJobResponse?>)> GetAllTaskJob();
}
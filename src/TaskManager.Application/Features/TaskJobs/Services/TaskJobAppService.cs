using AutoMapper;
using TaskManager.Application.Base.Models;
using TaskManager.Application.Base.Persistence;
using TaskManager.Application.Base.Services;
using TaskManager.Application.Features.TaskJobs.Repositories;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Responses;
using TaskManager.Application.Features.TaskJobs.Services.Contracts;

namespace TaskManager.Application.Features.TaskJobs.Services;

public class TaskJobAppService : AppService, ITaskJobAppService
{
    private readonly IMapper _mapper;
    private readonly ITaskJobRepository _taskJobRepository;

    public TaskJobAppService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITaskJobRepository taskJobRepository) : base(unitOfWork)
    {
        _mapper = mapper;
        _taskJobRepository = taskJobRepository;
    }

    public async Task<(Response, TaskJob?)> CreateTaskJob(CreateTaskJobRequest request, CancellationToken cancellationToken = default)
    {
        var taskJob = new TaskJob(request.Name, request.Description, request.DeliveryDate, request.EstimateHours);

        try
        {
            await _taskJobRepository.AddAsync(taskJob);

            await Commit();

            return (Response.Valid(), taskJob);
        }
        catch (Exception ex)
        {
            return (Response.Invalid("TaskJob", ex.Message), taskJob);
        }
    }

    public async Task<Response> RemoveTaskJob(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var taskJob = await _taskJobRepository.GetByIdAsync(id);

            if (taskJob is null)
                return Response.Invalid("TaskJob", "Task Job not found");

            _taskJobRepository.Remove(taskJob);

            await Commit();

            return Response.Valid();
        }
        catch (Exception ex)
        {
            return Response.Invalid("TaskJob", ex.Message);
        }
    }

    public async Task<(Response, TaskJob?)> UpdateTaskJob(UpdateTaskJobRequest request, CancellationToken cancellationToken = default)
    {
        var taskJob = await _taskJobRepository.GetByIdAsync(request.Id);

        try
        {
            if (taskJob is null)
                return (Response.Invalid("TaskJob", "Task Job not found"), taskJob);

            var taskJobMapper = _mapper.Map(request, taskJob);

            _taskJobRepository.Update(taskJobMapper);

            await Commit();

            return (Response.Valid(), taskJobMapper);
        }
        catch (Exception ex)
        {
            return (Response.Invalid("TaskJob", ex.Message), taskJob);
        }
    }

    public async Task<(Response, TaskJobResponse?)> GetTaskJobById(Guid id) => (Response.Valid(), _mapper.Map<TaskJobResponse?>(await _taskJobRepository.GetByIdAsync(id)));

    public async Task<(Response, IEnumerable<TaskJobResponse?>)> GetAllTaskJob() => (Response.Valid(), _mapper.Map<IEnumerable<TaskJobResponse?>>(await _taskJobRepository.GetAllAsync()));
}
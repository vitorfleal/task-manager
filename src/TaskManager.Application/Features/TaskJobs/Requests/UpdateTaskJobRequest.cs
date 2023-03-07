namespace TaskManager.Application.Features.TaskJobs.Requests;

public class UpdateTaskJobRequest : CreateTaskJobRequest
{
    public Guid Id { get; set; }
}
using TaskManager.Application.Features.TaskJobs.Requests;

namespace TaskManager.Application.Features.TaskJobs.Validators;

public class UpdateTaskJobValidator : TaskJobValidator<UpdateTaskJobRequest>
{
    public UpdateTaskJobValidator()
    {
        RuleRequiredFor(taskRequest => taskRequest.Id, "Id");
    }
}
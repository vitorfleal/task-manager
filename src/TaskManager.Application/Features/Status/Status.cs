using TaskManager.Application.Base.Models;
using TaskManager.Application.Features.TaskJobs;

namespace TaskManager.Application.Features.Status;

public class Status : Entity
{
    public Status(string? name)
    {
        Name = name;
    }

    public Status()
    {
    }

    public string? Name { get; private set; }

    public List<TaskJob>? TaskJobs { get; private set; }
}
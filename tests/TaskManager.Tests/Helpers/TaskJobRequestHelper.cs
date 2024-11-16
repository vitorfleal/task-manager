using TaskManager.Application.Features.TaskJobs;
using TaskManager.Application.Features.TaskJobs.Requests;

namespace TaskManager.Tests.Helpers;

public static class TaskJobRequestHelper
{

    public static TaskJob NewTaskJob() =>
        new("Task Job test 1", "Task Job test description 1", DateTime.UtcNow, 1);

    public static CreateTaskJobRequest CreateTaskJobRequest() =>
        new()
        {
            Name = "Task Job test 1",
            Description = "Task Job test description 1",
            DeliveryDate = DateTime.Now,
            EstimateHours = 1,
        };

    public static UpdateTaskJobRequest UpdateTaskJobRequest() =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Task Job update test 1",
            Description = "Task Job update test description 1",
            DeliveryDate = DateTime.Now,
            EstimateHours = 2,
        };
}
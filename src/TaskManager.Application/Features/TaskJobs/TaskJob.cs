using TaskManager.Application.Base.Models;

namespace TaskManager.Application.Features.TaskJobs;

public class TaskJob : Entity
{
    public TaskJob(string? name, string? description, DateTime? deliveryDate, int estimateHours, Guid statusId)
    {
        Name = name;
        Description = description;
        DeliveryDate = deliveryDate;
        EstimateHours = estimateHours;
        StatusId = statusId;
    }

    public TaskJob()
    {
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public int EstimateHours { get; private set; }
    public Guid StatusId { get; private set; }

    public Status.Status? Status { get; set; }
}
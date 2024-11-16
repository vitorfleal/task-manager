namespace TaskManager.Application.Features.TaskJobs.Responses;

public class TaskJobResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int EstimateHours { get; set; }
    public DateTime? CreatedDate { get; set; }
}
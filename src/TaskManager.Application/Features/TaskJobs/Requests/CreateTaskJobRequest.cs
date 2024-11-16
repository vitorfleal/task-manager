namespace TaskManager.Application.Features.TaskJobs.Requests;

public class CreateTaskJobRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int EstimateHours { get; set; }
}
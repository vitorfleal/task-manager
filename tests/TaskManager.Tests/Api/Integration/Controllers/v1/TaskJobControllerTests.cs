using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using TaskManager.Application.Base.Notifications;
using TaskManager.Application.Features.TaskJobs;
using TaskManager.Application.Features.TaskJobs.Repositories;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Responses;
using TaskManager.Tests.Helpers;

namespace TaskManager.Tests.Api.Integration.Controllers.v1;

public class TaskJobControllerTests : IntegrationTestBase<Program>
{
    private readonly HttpClient _client;

    public TaskJobControllerTests()
    {
        _client = GetTestAppClient();
    }

    [Fact(DisplayName = "Should return status 'Created' when create a task job valid")]
    public async Task CreateTaskJob_CreateATaskJobValid_ShouldReturnStatusCreated()
    {
        //Arrange
        var command = new CreateTaskJobRequest()
        {
            Name = "Task create test",
            Description = "Task create test valid",
            DeliveryDate = DateTime.UtcNow,
            EstimateHours = 1,
        };

        //Act
        var response = await _client.PostAsJsonAsync("/v1/task-job/", command);

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();

        var taskJobResponse = JsonConvert.DeserializeObject<TaskJobResponse>(content);

        var taskJobDb = await GetTaskJobById(taskJobResponse.Id);

        if (taskJobDb is not null)
        {
            taskJobDb.Name.Should().Be(command.Name);
            taskJobDb.Description.Should().Be(command.Description);
            taskJobDb.DeliveryDate.Should().Be(command.DeliveryDate);
            taskJobDb.EstimateHours.Should().Be(command.EstimateHours);
        }
    }

    [Fact(DisplayName = "Should return status 'No Content' when update a task job valid")]
    public async Task UpdateTaskJob_UpdateATaskJobValid_ShouldReturnStatusNoContent()
    {
        //Arrange
        var taskJob = await GetFirstTaskJob();

        var command = new UpdateTaskJobRequest()
        {
            Id = taskJob.Id,
            Name = "Task update test",
            Description = "Task update test valid",
            DeliveryDate = DateTime.UtcNow,
            EstimateHours = 2,
        };

        //Act
        var response = await _client.PutAsJsonAsync("/v1/task-job/", command);

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var taskJobDb = await GetTaskJobById(command.Id);

        if (taskJobDb is not null)
        {
            taskJobDb.Name.Should().Be(command.Name);
            taskJobDb.Description.Should().Be(command.Description);
            taskJobDb.DeliveryDate.Should().Be(command.DeliveryDate);
            taskJobDb.EstimateHours.Should().Be(command.EstimateHours);
        }
    }

    [Fact(DisplayName = "Should return status 'No Content' when remove a task job valid")]
    public async Task RemoveTaskJob_RemoveATaskJobValid_ShouldReturnStatusNoContext()
    {
        //Arrange
        var taskJob = await GetFirstTaskJob();

        //Act
        var response = await _client.DeleteAsync($"/v1/task-job/{taskJob.Id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var taskJobDb = await GetTaskJobById(taskJob.Id);
        taskJobDb.Should().BeNull();
    }

    [Fact(DisplayName = "Should return status 'Ok' when get a task job valid")]
    public async Task GetTaskJobById_GetATaskJobValid_ShouldReturnStatusIsValid()
    {
        //Arrange
        var taskJob = await GetFirstTaskJob();

        //Act
        var response = await _client.GetAsync($"/v1/task-job/{taskJob.Id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var taskJobResponse = JsonConvert.DeserializeObject<TaskJobResponse>(content);

        taskJobResponse.Should().NotBeNull();
        taskJobResponse?.Name.Should().Be(taskJob.Name);
        taskJobResponse?.Description.Should().Be(taskJob.Description);
        taskJobResponse?.DeliveryDate.Should().Be(taskJob.DeliveryDate);
        taskJobResponse?.EstimateHours.Should().Be(taskJob.EstimateHours);
    }

    [Fact(DisplayName = "Should return status 'Bad Request' when get a task job invalid")]
    public async Task GetTaskJobById_GetATaskJobInValid_ShouldReturnStatusBadRequest()
    {
        //Arrange
        var id = Guid.Empty;

        //Act
        var response = await _client.GetAsync($"/v1/task-job/{id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().Be("Task Id Invalid");
    }

    [Fact(DisplayName = "Should return status 'Not Found' when get a task job invalid")]
    public async Task GetTaskJobById_GetATaskJobInValid_ShouldReturnStatusNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"/v1/task-job/{id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();

        var taskJobResponse = JsonConvert.DeserializeObject<Notification>(content);

        taskJobResponse.Should().NotBeNull();
        taskJobResponse?.Code.Should().Be("TaskJob");
        taskJobResponse?.Description.Should().Be("TaskJob Not Found");
    }

    [Fact(DisplayName = "Should return status 'Ok' when get all task jobs valid")]
    public async Task GetAllTaskJobBy_GetAllATaskJobValid_ShouldReturnStatusIsValid()
    {
        //Arrange
        var taskJob = await GetFirstTaskJob();

        //Act
        var response = await _client.GetAsync($"/v1/task-job");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var taskJobResponse = JsonConvert.DeserializeObject<IEnumerable<TaskJobResponse>>(content);

        taskJobResponse.Should().NotBeNull();
        taskJobResponse.Should().HaveCount(1);
        taskJobResponse?.Select(x => x.Id).Should().Equal(taskJob.Id);
        taskJobResponse?.Select(x => x.Name).Should().BeEquivalentTo(taskJob.Name);
        taskJobResponse?.Select(x => x.Description).Should().BeEquivalentTo(taskJob.Description);
        taskJobResponse?.Select(x => x.DeliveryDate).Should().Equal(taskJob.DeliveryDate);
        taskJobResponse?.Select(x => x.EstimateHours).Should().Equal(taskJob.EstimateHours);
    }

    private async Task<TaskJob> GetFirstTaskJob()
    {
        using var scope = ServiceProvider?.CreateScope();

        if (scope is null)
            return new();

        var taskJobRepository = scope.ServiceProvider.GetRequiredService<ITaskJobRepository>();

        var taskJob = await taskJobRepository.GetAllAsync();

        return taskJob.First();
    }

    private async Task<TaskJob?> GetTaskJobById(Guid id)
    {
        using var scope = ServiceProvider?.CreateScope();

        if (scope is null)
            return null;

        var taskJobRepository = scope.ServiceProvider.GetRequiredService<ITaskJobRepository>();

        return await taskJobRepository.GetByIdAsync(id);
    }
}
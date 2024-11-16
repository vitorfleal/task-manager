using AutoMapper;
using FluentAssertions;
using Moq;
using TaskManager.Application.Base.Persistence;
using TaskManager.Application.Features.TaskJobs;
using TaskManager.Application.Features.TaskJobs.Repositories;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Services;
using TaskManager.Application.Features.TaskJobs.Services.Contracts;
using TaskManager.Tests.Helpers;

namespace TaskManager.Tests.Application.Features.TaskJobs;

public class TaskJobAppServiceTests
{

    [Fact(DisplayName = "Should return valid response when create a task job")]
    public async Task CreateTaskJob_CreateAValidTaskJob_ShouldReturnValidResponse()
    {
        // arrange
        var request = TaskJobRequestHelper.CreateTaskJobRequest();

        var (_, service) = TaskJobContextMock();

        // act
        var (response, createdTaskJob) = await service.CreateTaskJob(request);

        // assert
        createdTaskJob.Should().NotBeNull();
        createdTaskJob?.Id.Should().NotBe(Guid.Empty);
        createdTaskJob?.Name.Should().Be("Task Job test 1");
        createdTaskJob?.Description.Should().Be("Task Job test description 1");
        createdTaskJob?.EstimateHours.Should().Be(1);
        response?.IsValid().Should().BeTrue();
        response?.Notifications.Should().HaveCount(0);
    }

    [Fact(DisplayName = "Should return valid response when update a task job")]
    public async Task UpdateTaskJob_UpdateAValidTaskJob_ShouldReturnValidResponse()
    {
        // arrange
        var request = TaskJobRequestHelper.UpdateTaskJobRequest();

        var (_, service) = TaskJobContextMock(true, true, request);

        // act
        var (response, updateTaskJob) = await service.UpdateTaskJob(request);

        // assert
        updateTaskJob.Should().NotBeNull();
        updateTaskJob?.Id.Should().NotBe(Guid.Empty);
        updateTaskJob?.Name.Should().Be("Task Job update test 1");
        updateTaskJob?.Description.Should().Be("Task Job update test description 1");
        updateTaskJob?.EstimateHours.Should().Be(2);
        response?.IsValid().Should().BeTrue();
        response?.Notifications.Should().HaveCount(0);
    }

    [Fact(DisplayName = "Should return invalid response when update a task job return task job null")]
    public async Task UpdateTaskJob_UpdateAInValidTaskJob_ShouldReturnInValidResponse()
    {
        // arrange
        var request = TaskJobRequestHelper.UpdateTaskJobRequest();

        var (_, service) = TaskJobContextMock(true, false, request);

        // act
        var (response, updateTaskJob) = await service.UpdateTaskJob(request);

        // assert
        updateTaskJob.Should().BeNull();
        response?.IsValid().Should().BeFalse();
        response?.Notifications.Should().Contain(n => n.Description == "Task Job not found");
    }

    [Fact(DisplayName = "Should return invalid response when remove a task job")]
    public async Task RemoveTaskJob_UpdateAValidTaskJob_ShouldReturnValidResponse()
    {
        // arrange
        var request = TaskJobRequestHelper.UpdateTaskJobRequest();

        var (_, service) = TaskJobContextMock(false, true);

        // act
        var response = await service.RemoveTaskJob(request.Id);

        // assert
        response?.IsValid().Should().BeTrue();
        response?.Notifications.Should().HaveCount(0);
    }

    [Fact(DisplayName = "Should return invalid response when remove a task job return task job not found")]
    public async Task RemoveTaskJob_UpdateAInValidTaskJob_ShouldReturnInValidResponse()
    {
        // arrange
        var request = TaskJobRequestHelper.UpdateTaskJobRequest();

        var (_, service) = TaskJobContextMock();

        // act
        var response = await service.RemoveTaskJob(request.Id);

        // assert
        response?.IsValid().Should().BeFalse();
        response?.Notifications.Should().Contain(n => n.Description == "Task Job not found");
    }

    private static (Mock<ITaskJobRepository> RepositoryMock, ITaskJobAppService Service) TaskJobContextMock(
        bool setupMockMapper = false,
        bool setupMockTaskJob = false,
        UpdateTaskJobRequest? request = null)
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var taskJobRepositoryMock = new Mock<ITaskJobRepository>();

        var taskJob = TaskJobRequestHelper.NewTaskJob();

        unitOfWorkMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);

        if (setupMockTaskJob)
            taskJobRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(taskJob);


        if (setupMockMapper)
        {
            var expectedReturn = new TaskJob(request.Name, request.Description, request.DeliveryDate, request.EstimateHours);
            mapperMock.Setup(m => m.Map(It.IsAny<UpdateTaskJobRequest>(), It.IsAny<TaskJob>())).Returns(expectedReturn);
        }

        var taskJobService = new TaskJobAppService(unitOfWorkMock.Object, mapperMock.Object, taskJobRepositoryMock.Object);

        return (taskJobRepositoryMock, taskJobService);
    }
}
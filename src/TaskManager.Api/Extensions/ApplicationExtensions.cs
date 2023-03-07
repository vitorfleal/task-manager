using TaskManager.Application.Features.Status.Services;
using TaskManager.Application.Features.Status.Services.Contracts;
using TaskManager.Application.Features.TaskJobs.Services;
using TaskManager.Application.Features.TaskJobs.Services.Contracts;

namespace TaskManager.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Add handlers, services, repositories

        services.AddTransient<ITaskJobAppService, TaskJobAppService>();
        services.AddTransient<IStatusAppService, StatusAppService>();

        return services;
    }
}
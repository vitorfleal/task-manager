using TaskManager.Application.Features.TaskJobs.Services;
using TaskManager.Application.Features.TaskJobs.Services.Contracts;

namespace TaskManager.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITaskJobAppService, TaskJobAppService>();

        return services;
    }
}
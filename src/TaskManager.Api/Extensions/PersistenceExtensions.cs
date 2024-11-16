using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Base.Persistence;
using TaskManager.Application.Features.TaskJobs.Repositories;
using TaskManager.Infrastructure.Contexts;
using TaskManager.Infrastructure.Contexts.Persistence;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {

        if (!env.IsEnvironment("Testing"))
        {
            services.AddDbContext<TaskManagerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"), builder =>
                    builder.MigrationsAssembly("TaskManager.Infrastructure"))
                );
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<ITaskJobRepository, TaskJobRepository>();

        return services;
    }
}
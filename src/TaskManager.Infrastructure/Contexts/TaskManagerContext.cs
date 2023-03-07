using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.Application.Features.Status;
using TaskManager.Application.Features.TaskJobs;

namespace TaskManager.Infrastructure.Contexts;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
    {
    }

    public DbSet<TaskJob> TaskJobs => Set<TaskJob>();
    public DbSet<Status> Status => Set<Status>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
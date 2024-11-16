using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Application.Features.TaskJobs;

namespace TaskManager.Infrastructure.Contexts.Configurations;

public class TaskJobConfiguration : IEntityTypeConfiguration<TaskJob>
{
    public void Configure(EntityTypeBuilder<TaskJob> builder)
    {
        builder.ToTable(nameof(TaskJob));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.DeliveryDate).IsRequired();
    }
}
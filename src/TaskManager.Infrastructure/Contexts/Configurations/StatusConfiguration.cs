using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Application.Features.Status;

namespace TaskManager.Infrastructure.Contexts.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData
        (
          new Status("Na fila"),
          new Status("Em desenvolvimento"),
          new Status("Em homologação"),
          new Status("Concluído")
        );
    }
}
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Infrastructure.Contexts;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    partial class TaskManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Application.Features.Status.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fde2cc85-645d-49d6-8b04-60173f99e2f2"),
                            CreatedDate = new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(950),
                            Name = "Na fila"
                        },
                        new
                        {
                            Id = new Guid("216a7728-0057-447f-a064-3111937c6cc5"),
                            CreatedDate = new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(953),
                            Name = "Em desenvolvimento"
                        },
                        new
                        {
                            Id = new Guid("5eec74b8-b815-49b3-8eab-2b58f5569c64"),
                            CreatedDate = new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(954),
                            Name = "Em homologação"
                        },
                        new
                        {
                            Id = new Guid("bba97714-da40-45dc-ad32-a55efb5e6895"),
                            CreatedDate = new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(966),
                            Name = "Concluído"
                        });
                });

            modelBuilder.Entity("TaskManager.Application.Features.TaskJobs.TaskJob", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveryDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstimateHours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("TaskJob", (string)null);
                });

            modelBuilder.Entity("TaskManager.Application.Features.TaskJobs.TaskJob", b =>
                {
                    b.HasOne("TaskManager.Application.Features.Status.Status", "Status")
                        .WithMany("TaskJobs")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TaskManager.Application.Features.Status.Status", b =>
                {
                    b.Navigation("TaskJobs");
                });
#pragma warning restore 612, 618
        }
    }
}

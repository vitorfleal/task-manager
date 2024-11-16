using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Contexts;

namespace Acropolis.Tests.Api.Integration.Base;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<TaskManagerContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<TaskManagerContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTaskManagerTest");
            });

            AssertionOptions.FormattingOptions.MaxLines = 5000;

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
            appContext.Database.EnsureCreated();
            var databaseSeed = new TaskManagerDatabaseSeed(appContext);

            databaseSeed.SeedData();
        });
    }
}
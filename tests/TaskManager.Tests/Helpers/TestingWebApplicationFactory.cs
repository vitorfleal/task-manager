using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManager.Infrastructure.Contexts;

namespace TaskManager.Tests.Helpers;

public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<TaskManagerContext>()
                .UseInMemoryDatabase("InMemoryAcropolisTest", root)
                .UseApplicationServiceProvider(sp)
                .Options;
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var testSqlContext = scopedServices.GetRequiredService<TaskManagerContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<TestingWebApplicationFactory<TStartup>>>();

            testSqlContext.Database.EnsureDeleted();

            testSqlContext.Database.EnsureCreated();

            try
            {
                var databaseSeed = new TaskManagerDatabaseSeed(testSqlContext);
                databaseSeed.SeedData();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });

        return base.CreateHost(builder);
    }
}
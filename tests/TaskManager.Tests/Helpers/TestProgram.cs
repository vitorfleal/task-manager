﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TaskManager.Infrastructure.Contexts;

namespace TaskManager.Tests.Helpers;

public class TestProgramTestProgram<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(ConfigureTest);
        builder.ConfigureTestServices((services) =>
        {
            services.RemoveAll(typeof(IHostedService));
        });
    }

    public static void ConfigureTest(IServiceCollection services)
    {
        services.AddDbContext<TaskManagerContext>(options =>
        {
            options.UseInMemoryDatabase("TaskManagerTest");
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
    }
}
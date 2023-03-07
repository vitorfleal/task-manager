namespace TaskManager.Api.Extensions;

public static class SentryExtensions
{
    public static IWebHostBuilder AddSentry(this IWebHostBuilder webHostBuilder, IConfigurationRoot configurationRoot)
    {
        webHostBuilder.UseSentry((o =>
        {
            o.Dsn = configurationRoot.GetValue<string>("Sentry_Dsn");
            o.Debug = true;
            o.DiagnosticLevel = Sentry.SentryLevel.Debug;
            o.MaxRequestBodySize = Sentry.Extensibility.RequestSize.Always;
            o.SendDefaultPii = true;
            o.ServerName = Environment.MachineName;
            o.MinimumBreadcrumbLevel = LogLevel.Debug;
            o.MinimumEventLevel = LogLevel.Warning;
            o.SendClientReports = false;
            o.TracesSampleRate = 1.0;
        }));

        return webHostBuilder;
    }
}

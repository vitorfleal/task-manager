using TaskManager.Application.AutoMapper;

namespace TaskManager.Api.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ConfiguringMapperProfile));

        return services;
    }
}
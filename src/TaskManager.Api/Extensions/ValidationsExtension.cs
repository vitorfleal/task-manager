using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManager.Application.Features.TaskJobs.Validators;

namespace TaskManager.Api.Extensions;

public static class ValidationsExtension
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<CreateTaskJobValidator>();

        services.AddValidatorsFromAssemblyContaining<UpdateTaskJobValidator>();

        return services;
    }
}
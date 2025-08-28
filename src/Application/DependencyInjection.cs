using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Extensions;
using System.Reflection;
using Application.Behaviors;
using Mediator;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddServicesFromAssembly(assembly);
        services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
                options.PipelineBehaviors =
                [
                    typeof(LoggingBehavior<,>),
                    typeof(FluentValidationBehavior<,>)
                ];
            }
        );

        return services;
    }
}
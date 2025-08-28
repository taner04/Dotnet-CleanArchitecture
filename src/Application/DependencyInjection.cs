using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Extensions;
using System.Reflection;
using Application.CQRS.Behaviors;
using Domain.Common.Interfaces.DomainEvent;
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
                    typeof(FluentValidationBehavior<,>)
                ];
            }
        );

        return services;
    }
}
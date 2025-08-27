using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Extensions;
using System.Reflection;
using Domain.Common.Interfaces.DomainEvent;
using Mediator;

namespace Application;

/// <summary>
/// Provides extension methods for registering application services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services, validators, and mediator handlers from the current assembly.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddServicesFromAssembly(assembly);
        services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });
        return services;
    }
}
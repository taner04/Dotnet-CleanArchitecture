using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Attributes;
using SharedKernel.Enums;
using System.Reflection;

namespace SharedKernel.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ServiceInjection>() != null)
            .ToList();

        if (types.Count == 0) return services;

        foreach (var item in types)
        {
            var service = (ServiceInjection)item.GetCustomAttributes(typeof(ServiceInjection), true).FirstOrDefault()!;


            switch (service.Scope)
            {
                case ScopeType.Singleton:
                    services.AddSingleton(service.Interface, item);
                    break;
                case ScopeType.Scoped:
                    services.AddScoped(service.Interface, item);
                    break;
                case ScopeType.Transient:
                    services.AddTransient(service.Interface, item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(service.Scope),
                        $"Unsupported scope type: {service.Scope}");
            }
        }

        return services;
    }
}
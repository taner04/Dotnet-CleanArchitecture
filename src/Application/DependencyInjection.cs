using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Extensions;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IYourService, YourService>();

            services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

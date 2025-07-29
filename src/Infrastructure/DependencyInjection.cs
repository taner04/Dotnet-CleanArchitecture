using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel.Extensions;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.AddNpgsqlDbContext<ApplicationDbContext>("eshop");

            services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

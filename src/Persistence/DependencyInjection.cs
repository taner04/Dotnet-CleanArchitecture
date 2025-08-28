using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel.Extensions;
using System.Reflection;
using Persistence.Data;
using Persistence.Interceptor;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<ISaveChangesInterceptor, AuditableInterceptor>();
        services.AddSingleton<ISaveChangesInterceptor, SoftDeletableInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AggregateRootInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>().ToList();
            interceptors.ForEach(interceptor => { opt.AddInterceptors(interceptor); });

            opt.UseNpgsql(connectionString);
        });
        
        services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
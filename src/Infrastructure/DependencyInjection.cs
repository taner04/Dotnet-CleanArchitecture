using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel.Extensions;
using System.Reflection;

namespace Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services and dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services, including database context, interceptors, and assembly services.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="builder">The web application builder containing configuration and services.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Register save changes interceptors for auditing, soft deletion, and aggregate root handling.
        services.AddSingleton<ISaveChangesInterceptor, AuditableInterceptor>();
        services.AddSingleton<ISaveChangesInterceptor, SoftDeletableInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AggregateRootInterceptor>();
        // Register the application's database context and add interceptors.
        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>().ToList();
            interceptors.ForEach(interceptor => { opt.AddInterceptors(interceptor); });
            opt.UseNpgsql(builder.Configuration.GetConnectionString("eshop"));
        });
        // Enrich the database context with additional configuration.
        builder.EnrichNpgsqlDbContext<ApplicationDbContext>();
        // Register services from the current assembly.
        services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
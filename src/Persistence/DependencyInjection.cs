using Application.Common.Abstraction.Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Interceptors;
using Shared.Aspire;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AggregateRootInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, opt) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>().ToList();
            interceptors.ForEach(interceptor => { opt.AddInterceptors(interceptor); });

            opt.EnableSensitiveDataLogging();
            opt.EnableDetailedErrors();
            opt.UseNpgsql(configuration.GetConnectionString(AspireConstants.ApplicationDb));
        });

        return services;
    }
}
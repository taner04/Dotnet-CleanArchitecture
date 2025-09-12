using Application.Abstraction;
using Application.Abstraction.Infrastructure;
using Application.Abstraction.Persistence;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITokenService<User>, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        
        
        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AggregateRootInterceptor>();
        
        services.AddDbContext<IBudgetDbContext, BudgetDbContext>((sp, opt) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>().ToList();
            interceptors.ForEach(interceptor => { opt.AddInterceptors(interceptor); });
            
            opt.EnableSensitiveDataLogging();
            opt.EnableDetailedErrors();
            opt.UseNpgsql(connectionString);
        });
        
        return services;
    }
}
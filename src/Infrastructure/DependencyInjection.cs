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

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ISaveChangesInterceptor, UpdateAuditableInterceptor>();
            builder.Services.AddDbContext<ApplicationDbContext>((sp, opt) =>
            {
                opt.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
                opt.UseNpgsql(builder.Configuration.GetConnectionString("eshop"));
            });

            builder.EnrichNpgsqlDbContext<ApplicationDbContext>();

            services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

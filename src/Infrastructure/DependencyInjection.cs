using Application.Common.Abstraction.Infrastructure;
using Infrastructure.Utils;
using Infrastructure.Utils.Token;
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
}
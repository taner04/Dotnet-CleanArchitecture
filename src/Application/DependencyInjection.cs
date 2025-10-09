using Application.Common;
using Application.Common.Behaviours;
using Application.CQRS.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterUser.Command>, RegisterUser.Validator>();
        services.AddScoped<IValidator<LoginUser.Query>, LoginUser.Validator>();

        services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
                options.PipelineBehaviors =
                [
                    typeof(LoggingBehaviour<,>),
                    typeof(FluentValidationBehaviour<,>)
                ];
            }
        );

        services.AddScoped<UserService>();

        return services;
    }
}
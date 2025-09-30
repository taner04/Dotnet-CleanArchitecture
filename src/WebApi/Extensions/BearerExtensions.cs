using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared.WebApi;
using WebApi.Bearer;

namespace WebApi.Extensions;

public static class BearerExtensions
{
    public static IServiceCollection AddBearerScheme(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

        services.AddAuthorization();

        var jwtSettings = configuration.GetSection("JWTSettings").Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings are not configured properly.");
        
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = jwtSettings.ToTokenValidationParameters();
            });

        return services;
    }
}
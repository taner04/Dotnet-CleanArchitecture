using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Bearer;

namespace WebApi.Extensions;

public static class BearerExtensions
{
    public static IServiceCollection AddBearerScheme(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

        services.AddAuthorization();

        var secretKey = configuration["JwtSettings:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new Exception("JWT secret key is missing");
        }

        var issuer = configuration["JwtSettings:Issuer"];
        if (string.IsNullOrEmpty(issuer))
        {
            throw new Exception("JWT issuer is missing");
        }

        var audience = configuration["JwtSettings:Audience"];
        if (string.IsNullOrEmpty(audience))
        {
            throw new Exception("JWT audience is missing");
        }

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });

        return services;
    }
}
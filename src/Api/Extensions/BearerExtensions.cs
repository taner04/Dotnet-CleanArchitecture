using Api.Bearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions
{
    public static class BearerExtensions
    {
        public static IServiceCollection AddBearerScheme(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenApi("v1", options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException(nameof(configuration), "JwtSettings:SecretKey is missing"))),
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                    };
                });

            return services;
        }
    }
}

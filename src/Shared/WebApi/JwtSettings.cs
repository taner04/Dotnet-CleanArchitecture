using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shared.WebApi;

public class JwtSettings(string secretKey, string issuer, string audience)
{
    /// <summary>
    ///     Gets the secret key used to sign the JWT tokens.
    /// </summary>
    public string SecretKey { get; init; } = secretKey;

    /// <summary>
    ///     Gets the issuer of the JWT tokens.
    /// </summary>
    public string Issuer { get; init; } = issuer;

    /// <summary>
    ///     Gets the audience for which the JWT tokens are intended.
    /// </summary>
    public string Audience { get; init; } = audience;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the token's lifetime.
    /// </summary>
    public bool ValidateLifetime { get; init; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether the token must have an expiration time.
    /// </summary>
    public bool RequireExpirationTime { get; init; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the signing key.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; init; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the issuer.
    /// </summary>
    public bool ValidateIssuer { get; init; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the audience.
    /// </summary>
    public bool ValidateAudience { get; init; } = true;

    /// <summary>
    ///     Creates a <see cref="TokenValidationParameters"/> instance based on the current JWT settings.
    /// </summary>
    /// <returns>
    ///     A configured <see cref="TokenValidationParameters"/> object for validating JWT tokens.
    /// </returns>
    public TokenValidationParameters ToTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            ValidateIssuer = ValidateIssuer,
            ValidateAudience = ValidateAudience,
            RequireExpirationTime = RequireExpirationTime,
            ValidateLifetime = ValidateLifetime
        };
    }
}
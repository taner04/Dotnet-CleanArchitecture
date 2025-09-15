namespace Infrastructure.Utils.Token;

public class JwtSettings(string secretKey, string issuer, string audience)
{
    /// <summary>
    /// Gets the secret key used to sign the JWT tokens.
    /// </summary>
    public string SecretKey { get; init; } = secretKey;

    /// <summary>
    /// Gets the issuer of the JWT tokens.
    /// </summary>
    public string Issuer { get; init; } = issuer;

    /// <summary>
    /// Gets the audience for which the JWT tokens are intended.
    /// </summary>
    public string Audience { get; init; } = audience;
}
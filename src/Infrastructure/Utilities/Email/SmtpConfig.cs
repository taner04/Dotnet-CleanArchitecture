using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utilities.Email;

public class SmtpConfig
{
    private readonly IConfiguration _configuration;

    public SmtpConfig(IConfiguration configuration)
    {
        _configuration = configuration;

        Host = _configuration["Smtp:Host"] ?? throw new ArgumentNullException("Smtp:Host is not configured");
        Port = int.TryParse(_configuration["Smtp:Port"], out var port)
            ? port
            : throw new ArgumentNullException("Smtp:Port is not configured");
    }

    public string Host { get; init; }
    public int Port { get; init; }
}
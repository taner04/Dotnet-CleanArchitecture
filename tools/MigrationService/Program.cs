using Persistence.Data;
using Shared.Aspire;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationService.MigrationService>();

builder.Services.AddOpenTelemetry().WithTracing(t =>
{
    t.AddSource(MigrationService.MigrationService.ActivitySourceName);
});

builder.AddNpgsqlDbContext<ApplicationDbContext>(AspireConstants.ApplicationDb);

var host = builder.Build();

host.Run();
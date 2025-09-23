using Persistence.Data;
using ServiceDefaults;
using SharedKernel;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationService.MigrationService>();

builder.Services.AddOpenTelemetry().WithTracing(t =>
{
    t.AddSource(MigrationService.MigrationService.ActivitySourceName);
});

builder.AddNpgsqlDbContext<BudgetDbContext>(AspireConstants.BudgetDb);

var host = builder.Build();

host.Run();
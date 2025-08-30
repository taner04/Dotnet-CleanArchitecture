using MigrationService;
using eShop.ServiceDefaults;
using Persistence.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<MigrationService.MigrationService>();

builder.Services.AddOpenTelemetry().WithTracing(t =>
{
    t.AddSource(MigrationService.MigrationService.ActivitySourceName);
});

builder.AddNpgsqlDbContext<ApplicationDbContext>("eshop");

var host = builder.Build();

host.Run();
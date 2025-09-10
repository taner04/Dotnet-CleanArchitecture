using Application.Abstraction;
using Infrastructure.Persistence.Data.Application;
using Infrastructure.Persistence.Data.Finance;
using MigrationService;
using SharedKernel;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationService<ApplicationDbContext>>();
builder.Services.AddHostedService<MigrationService<BudgetDbContext>>();

builder.Services.AddOpenTelemetry().WithTracing(t =>
{
    t.AddSource(MigrationService<ApplicationDbContext>.ActivitySourceName);
    t.AddSource(MigrationService<BudgetDbContext>.ActivitySourceName);
});

builder.AddNpgsqlDbContext<ApplicationDbContext>(Constants.ApplicationDb);
builder.AddNpgsqlDbContext<BudgetDbContext>(Constants.BudgetDb);

var host = builder.Build();

host.Run();
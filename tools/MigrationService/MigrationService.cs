using System.Diagnostics;
using Application.Abstraction;
using Application.Abstraction.Persistence;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MigrationService;

public class MigrationService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private readonly ActivitySource _sActivitySource = new(ActivitySourceName);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = _sActivitySource.StartActivity(ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);
        }
        catch (Exception e)
        {
            activity?.AddException(e);
            throw;
        }
        finally
        {
            applicationLifetime.StopApplication();
        }
    }

    private static async Task RunMigrationAsync(BudgetDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => { await dbContext.Database.MigrateAsync(cancellationToken); });
    }
}
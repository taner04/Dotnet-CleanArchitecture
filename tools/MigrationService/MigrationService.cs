using System.Diagnostics;
using Application.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MigrationService;

public class MigrationService<TDbContext>(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime) : BackgroundService
    where TDbContext : IDbContext
{
    public const string ActivitySourceName = $"{nameof(TDbContext)} Migrations";
    private readonly ActivitySource _sActivitySource = new(ActivitySourceName);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = _sActivitySource.StartActivity(ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

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

    private static async Task RunMigrationAsync(TDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => { await dbContext.Database.MigrateAsync(cancellationToken); });
    }
}
using System.Diagnostics;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace MigrationService;

public class MigrationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _applicationLifetime;
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource SActivitySource = new(ActivitySourceName);


    public MigrationService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = SActivitySource.StartActivity(ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);
            await SeedDataAsync(dbContext, stoppingToken);
        }
        catch (Exception e)
        {
            activity?.AddException(e);
            throw;
        }
        finally
        {
            _applicationLifetime.StopApplication();
        }
    }

    private static async Task RunMigrationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => { await dbContext.Database.MigrateAsync(cancellationToken); });
    }

    private static async Task SeedDataAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var productsSet = dbContext.Set<Product>();
            if (!productsSet.Any())
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                await productsSet.AddRangeAsync(GetProducts(), cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
        });
    }

    private static List<Product> GetProducts()
    {
        return
        [
            Product.TryCreate("Apple MacBook Air M2", "Lightweight laptop with Apple's M2 chip", 1199.99m, 10),
            Product.TryCreate("Samsung Galaxy S23", "Flagship Android smartphone", 949.00m, 15),
            Product.TryCreate("Logitech MX Master 3S", "Advanced wireless mouse", 99.99m, 20),
            Product.TryCreate("Sony WH-1000XM5", "Noise-cancelling over-ear headphones", 349.99m, 5),
            Product.TryCreate("Amazon Echo Dot (5th Gen)", "Smart speaker with Alexa", 49.99m, 30),
            Product.TryCreate("Dell UltraSharp U2723QE", "27-inch 4K monitor", 679.99m, 8),
            Product.TryCreate("Anker PowerCore 20100", "Portable charger 20100mAh", 49.95m, 25),
            Product.TryCreate("Canon EOS R6", "Full-frame mirrorless camera", 2499.00m, 12),
            Product.TryCreate("Nintendo Switch", "Handheld gaming console with OLED screen", 349.99m, 18),
            Product.TryCreate("Kindle Paperwhite Signature", "E-reader with 32GB storage and warm light", 189.99m, 14)
        ];
    }
}
using Projects;
using SharedKernel;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("database").WithPgAdmin();

var budgetDb = db.AddDatabase(AspireConstants.BudgetDb);

var migration = builder.AddProject<MigrationService>(AspireConstants.MigrationService)
    .WithReference(budgetDb)
    .WaitFor(budgetDb);

builder.AddProject<WebApi>(AspireConstants.BudgetApi)
    .WithReference(budgetDb)
    .WaitFor(budgetDb)
    .WaitForCompletion(migration);

builder.Build().Run();
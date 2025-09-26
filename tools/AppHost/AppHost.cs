using Projects;
using Shared.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("database").WithPgAdmin();

var budgetDb = db.AddDatabase(AspireConstants.ApplicationDb);

var migration = builder.AddProject<MigrationService>(AspireConstants.MigrationService)
    .WithReference(budgetDb)
    .WaitFor(budgetDb);

builder.AddProject<WebApi>(AspireConstants.WebApi)
    .WithReference(budgetDb)
    .WaitFor(budgetDb)
    .WaitForCompletion(migration);

builder.Build().Run();
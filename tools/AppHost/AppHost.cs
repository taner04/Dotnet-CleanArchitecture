using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("database").WithPgAdmin();

var budgetDb = db.AddDatabase("BudgetDb");

var migration = builder.AddProject<MigrationService>("migration-service")
    .WithReference(budgetDb)
    .WaitFor(budgetDb);

builder.AddProject<Api>("api")
    .WithReference(budgetDb)
    .WaitFor(budgetDb)
    .WaitForCompletion(migration);

builder.Build().Run();

using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("database").WithPgAdmin();

var budgetDb = db.AddDatabase("BudgetDb");
var applicationDb = db.AddDatabase("ApplicationDb");

var migration = builder.AddProject<MigrationService>("migration-service")
    .WithReference(budgetDb)
    .WaitFor(budgetDb)
    .WithReference(applicationDb)
    .WaitFor(applicationDb);

builder.AddProject<Api>("api")
    .WithReference(budgetDb)
    .WaitFor(budgetDb)
    .WithReference(applicationDb)
    .WaitFor(applicationDb)
    .WaitForCompletion(migration);

builder.Build().Run();

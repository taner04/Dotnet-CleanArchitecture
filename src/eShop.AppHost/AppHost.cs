var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db").WithPgAdmin();

var eshopDb = db.AddDatabase("eshop");

var migration = builder.AddProject<Projects.MigrationService>("migration-service")
    .WithReference(eshopDb)
    .WaitFor(eshopDb);

builder.AddProject<Projects.Api>("api")
    .WithReference(eshopDb)
    .WaitFor(eshopDb)
    .WaitFor(migration);

builder.Build().Run();
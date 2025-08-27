var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db").WithPgAdmin();

var eshopDb = db.AddDatabase("eshop");

builder.AddProject<Projects.Api>("api")
    .WithReference(eshopDb)
    .WaitFor(eshopDb);

builder.Build().Run();
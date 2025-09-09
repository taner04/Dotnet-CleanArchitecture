using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Api>("api");

builder.Build().Run();

using Api;
using Api.Extensions;
using Application;
using DotNetEnv;
using eShop.ServiceDefaults;
using Infrastructure;
using Infrastructure.Persistence.Extensions;
using Scalar.AspNetCore;


if (File.Exists(".env.dev")) Env.Load(".env.dev");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.AddServiceDefaults();

builder.Services.AddBearerScheme(configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(config =>
{
    config.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});

builder.Services.AddInfrastructure(builder);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Layout = ScalarLayout.Classic;
        opt.Theme = ScalarTheme.Mars;
        opt.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.Http11);
    });
    app.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();
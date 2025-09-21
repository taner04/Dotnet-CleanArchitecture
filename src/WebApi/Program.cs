using Application;
using Infrastructure;
using JetBrains.Annotations;
using ServiceDefaults;
using SharedKernel;
using WebApi.ExceptionHandler;
using WebApi.Extensions;

IConfigurationRoot configurationRoot;
#if DEBUG
configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .Build();
#else
configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
#endif

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString(AspireConstants.BudgetDb)!);
builder.Services.AddBearerScheme(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.AddScalarApiReference();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();

app.Run();

namespace WebApi
{
    [UsedImplicitly]
    public partial class Program; // For integration tests
}
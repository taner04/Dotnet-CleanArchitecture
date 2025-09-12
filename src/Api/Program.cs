using Api.ExceptionHandler;
using Api.Extensions;
using Application;
using DotNetEnv;
using Infrastructure;
using ServiceDefaults;
using SharedKernel;

if (File.Exists(".env.dev"))
{
    Env.Load(".env.dev");
}

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString(Constants.BudgetDb)!);
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
using Application;
using Infrastructure;
using JetBrains.Annotations;
using Persistence;
using SharedKernel.Aspire;
using WebApi.ExceptionHandler;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
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
    public class Program; // For integration tests
}
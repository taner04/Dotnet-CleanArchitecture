using System.Data.Common;
using Aspire.Hosting;
using Aspire.Hosting.Postgres;
using DotNetEnv;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SharedKernel;

namespace Api.IntegrationTests.Common;

[UsedImplicitly]
public class WebApiFactory(DbConnection dbConnection) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(opt =>
        {

        });
        
        builder.UseSetting($"ConnectionStrings:{AspireConstants.BudgetDb}", dbConnection.ConnectionString);
    }
}
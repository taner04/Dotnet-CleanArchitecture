using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SharedKernel;
using WebApi;

namespace Api.IntegrationTests.Common;

[UsedImplicitly]
public class WebApiFactory(DbConnection dbConnection) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(opt => { });

        builder.UseSetting($"ConnectionStrings:{AspireConstants.BudgetDb}", dbConnection.ConnectionString);
    }
}
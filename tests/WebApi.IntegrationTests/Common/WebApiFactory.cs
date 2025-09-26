using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Aspire;

namespace WebApi.IntegrationTests.Common;

public class WebApiFactory(DbConnection dbConnection) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(opt => { });

        builder.UseSetting($"ConnectionStrings:{AspireConstants.ApplicationDb}", dbConnection.ConnectionString);
    }
}
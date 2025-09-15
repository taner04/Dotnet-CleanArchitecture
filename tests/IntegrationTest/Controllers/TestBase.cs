using System.Runtime.Remoting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTest.Controllers;

public abstract class TestBase(ApiFactory factory) : IClassFixture<ApiFactory>
{
    protected readonly ApiFactory Factory = factory;
    
    private const string BaseUrl = "http://localhost:5014/api/";
    
    protected const string AuthenticationRoute = $"{BaseUrl}/auth";
    protected const string UsersRoute = $"{BaseUrl}/users";
    protected const string AccountsRoute = $"{BaseUrl}/accounts";
    protected const string TransactionsRoute = $"{BaseUrl}/transactions";

    protected static StringContent CreateBody(object obj) 
        => new(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json");
}
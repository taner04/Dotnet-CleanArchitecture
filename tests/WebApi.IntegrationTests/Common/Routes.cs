namespace Api.IntegrationTests.Common;

public static class Routes
{
    private const string AuthRoute = "auth";
    private const string UsersRoute = "users";
    private const string AccountRoute = "accounts";
    private const string TransactionsRoute = "transactions";

    public static string RegisterRoute => AuthRoute +  "/register";
    public static string LoginRoute => AuthRoute +  "/login";
    public static string RefreshTokenRoute => AuthRoute +  "/refresh-token";
}
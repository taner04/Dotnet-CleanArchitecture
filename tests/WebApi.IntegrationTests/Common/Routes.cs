namespace Api.IntegrationTests.Common;

public static class Routes
{
    public static class Auth
    {
        public const string Register = "auth/register";
        public const string Login = "auth/login";
        public const string RefreshToken = "auth/refresh-token";
    }
    
    public static class Account
    {
        public const string GetBalance = "accounts/get-balance";
    }

    public static class Transaction
    {
        public const string GetAll = "transactions/get-all";
        public const string Add = "transactions/add";
    }

    public static class User
    {
        public const string UpdateEmailNotification = "users/update-email-notification";
        public const string ChangeEmail = "users/change-email";
        public const string GetUserData = "users/get-user-data";
    }
}
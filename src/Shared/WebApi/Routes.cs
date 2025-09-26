namespace Shared.WebApi;

public static class Routes
{
    public static class Authentication
    {
        private const string Base = "auth";
        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
        public const string RefreshToken = $"{Base}/refresh-token";
    }

    public static class Account
    {
        private const string Base = "accounts";
        public const string GetBalance = $"{Base}/get-balance";
    }

    public static class Transaction
    {
        private const string Base = "transactions";
        public const string GetAll = $"{Base}/get-all";
        public const string Add = $"{Base}/add";
    }

    public static class User
    {
        private const string Base = "users";
        public const string UpdateEmailNotification = $"{Base}/update-email-notification";
        public const string ChangeEmail = $"{Base}/change-email";
        public const string GetUserData = $"{Base}/get-user-data";
    }
}
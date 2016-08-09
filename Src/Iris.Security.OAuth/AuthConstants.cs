namespace Iris.Security.OAuth
{
    public static class AuthConstants
    {
        public static class ClaimTypes
        {
            public const string Scope = "urn:scope";
            public const string Surname = "urn:user:surname";
            public const string GivenName = "urn:user:given_name";
            public const string Email = "urn:user:email";
            public const string AccountName = "urn:user:account_name";
            public const string UserId = "urn:user:user_id";
        }

        public static class ResponseTypes
        {
            public const string Code = "code";
            public const string Token = "token";
        }

        public static class TokenTypes
        {
            public const string Bearer = "bearer";
        }

        public static class Extra
        {
            public const string ClientId = "client_id";
            public const string RedirectUri = "redirect_uri";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;

namespace Iris.Security.OAuth.Services
{
    public class AccessTokenFactory
    {
        private static DateTimeOffset UtcNow { get { return SystemClock.UtcNow; } }
        private static readonly SystemClock SystemClock = new SystemClock();

        public static ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public object BuildToken(ICollection<Claim> claims, string authenticationType)
        {
            if(claims == null || claims.Count == 0 || String.IsNullOrWhiteSpace(authenticationType))
                throw new AuthenticationException("The current user is not authenticated or is not registered.");

            var identity = new ClaimsIdentity(claims, authenticationType, AuthConstants.ClaimTypes.AccountName, AuthConstants.ClaimTypes.Scope);
            return BuildToken(identity, TimeSpan.FromHours(18));
        }

        public object BuildToken(ClaimsIdentity identity, TimeSpan validFor)
        {
            Mandate.ParameterNotNull(identity, "identity");

            AuthenticationTicket ticket = GetAuthenticationTicket(identity, validFor);
            var token = AccessTokenFormat.Protect(ticket);

            return new
            {
                token_type = AuthConstants.TokenTypes.Bearer,
                access_token = token,
            };
        }

        private static AuthenticationTicket GetAuthenticationTicket(ClaimsIdentity identity, TimeSpan validFor)
        {
            var signin = new AuthenticationResponseGrant(identity, new AuthenticationProperties());
            signin.Properties.IssuedUtc = UtcNow;
            signin.Properties.ExpiresUtc = UtcNow.Add(validFor);
            signin.Properties.Dictionary[AuthConstants.Extra.ClientId] = "Global";

            return new AuthenticationTicket(signin.Identity, signin.Properties);
        }
    }
}

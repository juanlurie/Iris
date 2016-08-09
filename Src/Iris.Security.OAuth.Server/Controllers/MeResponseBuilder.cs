using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Iris.Security.OAuth.Server.Controllers
{
    public static class MeResponseBuilder
    {
        public static object BuildMeResponse(this IPrincipal principal)
        {
            return new
            {
                user = principal.BuildUserResponse(),
                claims = principal.BuildClaimsResponse()
            };
        }

        private static object BuildUserResponse(this IPrincipal principal)
        {
            return new
            {
                id = principal.GetClaimValue(AuthConstants.ClaimTypes.UserId) ?? String.Empty,
                accountName = principal.GetClaimValue(AuthConstants.ClaimTypes.AccountName) ?? String.Empty,
                givenName = principal.GetClaimValue(AuthConstants.ClaimTypes.GivenName) ?? String.Empty,
                surname = principal.GetClaimValue(AuthConstants.ClaimTypes.Surname) ?? String.Empty,
                email = principal.GetClaimValue(AuthConstants.ClaimTypes.Email) ?? String.Empty
            };
        }

        private static object[] BuildClaimsResponse(this IPrincipal principal)
        {
            return principal
                .GetClaims()
                .Where(IsSopeClaim())
                .Select(BuildClaim)
                .ToArray();
        }

        private static Func<Claim, bool> IsSopeClaim()
        {
            return m => m.Type.Equals(AuthConstants.ClaimTypes.Scope);
        }

        private static object BuildClaim(Claim claim)
        {
            return new
            {
                claim.Type,
                claim.Value,
            };
        }
    }
}
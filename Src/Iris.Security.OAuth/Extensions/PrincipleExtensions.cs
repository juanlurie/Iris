using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Iris.Security.OAuth;

// ReSharper disable CheckNamespace
namespace System.Security.Principal
// ReSharper restore CheckNamespace
{
    public static class PrincipleExtensions
    {
        private static ClaimsPrincipal GetClaimsPrincipal(IPrincipal principal)
        {
            return principal as ClaimsPrincipal;
        }

        public static IEnumerable<Claim> GetClaims(this IPrincipal principal)
        {
            var claimsPrincipal = GetClaimsPrincipal(principal);

            if (principal == null)
            {
                return new Claim[0];
            }

            return claimsPrincipal.Claims;
        }

        public static IEnumerable<string> GetClaimValues(this IPrincipal principal, string claimType)
        {
            return principal.GetClaims()
                .Where(c => c.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase))
                .Select(claim => claim.Value);
        }

        public static string GetClaimValue(this IPrincipal principal, string claimType)
        {
            var claims = GetClaimValues(principal, claimType).ToArray();

            return claims.Any() ? claims[0] : String.Empty;
        }

        public static bool HasClaim(this IPrincipal principal, Predicate<Claim> match)
        {
            var claimsPrincipal = GetClaimsPrincipal(principal);

            if (claimsPrincipal == null)
            {
                return false;
            }

            return claimsPrincipal.HasClaim(match);
        }

        public static bool HasClaim(this IPrincipal principal, string type, string value)
        {
            var claimsPrincipal = GetClaimsPrincipal(principal);

            if (claimsPrincipal == null)
            {
                return false;
            }

            return claimsPrincipal.HasClaim(type, value);
        }

        public static Guid UserId(this IPrincipal principal)
        {
            var claimsPrincipal = GetClaimsPrincipal(principal);

            if (claimsPrincipal == null)
            {
                return Guid.Empty; ;
            }

            Claim userIdClaim = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type.Equals(AuthConstants.ClaimTypes.UserId, StringComparison.InvariantCultureIgnoreCase));

            if (userIdClaim == null)
            {
                return Guid.Empty;
            }

            Guid userId;
            Guid.TryParse(userIdClaim.Value, out userId);
            return userId;
        }
    }
}
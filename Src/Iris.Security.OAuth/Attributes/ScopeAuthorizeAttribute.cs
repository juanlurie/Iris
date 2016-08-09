using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Iris.Security.OAuth.Attributes
{
    public class ScopeAuthorizeAttribute : AuthorizeAttribute
    {
        private static string[] defaultScopes;
        private readonly string[] scopes;

        public static void SetDefaultScopes(params string[] scopes)
        {
            if (scopes == null)
                throw new ArgumentNullException("scopes");

            defaultScopes = scopes;
        }

        public ScopeAuthorizeAttribute(params string[] scopes)
        {
            if (scopes == null && defaultScopes == null)
            {
                throw new ArgumentNullException("scopes");
            }

            this.scopes = scopes ?? defaultScopes;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null
                || actionContext.ControllerContext == null
                || actionContext.ControllerContext.RequestContext == null
                || actionContext.ControllerContext.RequestContext.Principal == null)
            {
                return false;
            }

            var principal = actionContext.ControllerContext.RequestContext.Principal as ClaimsPrincipal;

            if (principal == null)
            {
                return false;
            }

            if (scopes.Length == 0)
            {
                return true;
            }

            var grantedScopes = principal.Claims.Where(claim => claim.Type.Equals(AuthConstants.ClaimTypes.Scope))
                .Select(claim => claim.Value)
                .ToList();

            return scopes.Any(scope => grantedScopes.Contains(scope, StringComparer.OrdinalIgnoreCase));
        }
    }
}
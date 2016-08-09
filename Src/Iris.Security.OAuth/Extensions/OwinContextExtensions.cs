// ReSharper disable CheckNamespace

using System.Web;
using Microsoft.Owin;

namespace System.Security.Principal
// ReSharper restore CheckNamespace
{
    public static class OwinContextExtensions
    {
        public static IPrincipal GetAuthorizedPrinciple(this IOwinContext context)
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication.AuthenticationResponseGrant;

            if (authentication == null)
            {
                return null;
            }

            return authentication.Principal;
        }
    }
}

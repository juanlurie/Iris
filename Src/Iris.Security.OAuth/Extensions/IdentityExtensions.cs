using System.DirectoryServices.AccountManagement;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace System.Web.Http
// ReSharper restore CheckNamespace
{
    public static class IdentityExtensions
    {
        public static Task<UserContext> GetUserContextAsync(this IIdentity identity)
        {
            return Task.Run(() => GetUserContext(identity));
        }

        public static UserContext GetUserContext(this IIdentity identity)
        {
            var ctx = new PrincipalContext(Context(identity));

            UserPrincipal userPrinciple = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, identity.Name);

            if (userPrinciple == null)
            {
                throw new AuthenticationException(String.Format("Unable to locate the user principle for user {0}", identity.Name));
            }

            return new UserContext(userPrinciple);
        }

        private static ContextType Context(IIdentity identity)
        {
            var contextType = ContextType.Domain;

            if (identity.IsLocalUser())
            {
                contextType = ContextType.Machine;
            }

            return contextType;
        }

        public static bool IsLocalUser(this IIdentity identity)
        {
            string strMachineName = Environment.MachineName;
            return identity.Name.ToUpper().StartsWith(strMachineName.ToUpper());
        }

        public static string[] SplitUserName(this IIdentity identity)
        {
            return identity.Name.Split('\\');
        }
    }
}
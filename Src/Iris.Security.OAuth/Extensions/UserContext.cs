// ReSharper disable CheckNamespace

using System.DirectoryServices.AccountManagement;
using System.Runtime.Serialization;

namespace System.Security.Principal
// ReSharper restore CheckNamespace
{
    [DataContract]
    public class UserContext
    {
        public string Tennant { get; protected set; }
        public string Username { get; protected set; }
        public string GivenName { get; protected set; }
        public string Surname { get; protected set; }
        public string EmailAddress { get; protected set; }

        public UserContext(UserPrincipal userPrincipal)
        {
            GivenName = userPrincipal.GivenName ?? String.Empty;
            Surname = userPrincipal.Surname ?? String.Empty;
            EmailAddress = userPrincipal.EmailAddress ?? String.Empty;
            Tennant = GetTennant(userPrincipal);
            Username = GetUsername(userPrincipal);
        }

        private string GetUsername(UserPrincipal userPrincipal)
        {
            if (userPrincipal.Context.ContextType == ContextType.Domain)
            {
                return userPrincipal.SamAccountName;
            }

            return userPrincipal.Name;
        }

        private string GetTennant(UserPrincipal userPrincipal)
        {
            if (userPrincipal.Context.ContextType == ContextType.Domain)
            {
                var atSymbolIndex = userPrincipal.UserPrincipalName.IndexOf('@') + 1;
                var firstDotIndex = userPrincipal.UserPrincipalName.IndexOf('.');

                return userPrincipal.UserPrincipalName.Substring(atSymbolIndex, firstDotIndex - atSymbolIndex);
            }

            return userPrincipal.Context.ConnectedServer;
        }
    }
}
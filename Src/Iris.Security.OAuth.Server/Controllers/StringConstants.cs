using System.Collections.Generic;

namespace Iris.Security.OAuth.Server.Controllers
{
    public static class StringConstants
    {
        private static List<string> defaultScopes;
        private static List<string> defaultRoles;

        public static IEnumerable<string> DefaultRoles { get { return defaultRoles; } }
        public static IEnumerable<string> DefaultScopes { get { return defaultScopes; } }



        static StringConstants()
        {
            defaultRoles = new List<string>();
            defaultScopes = new List<string>();

            defaultRoles.Add("System Administrator");

            defaultScopes.Add("system.administrator");
        }
    }
}
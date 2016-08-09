using System;

namespace Iris.Security.OAuth.Server.Controllers
{
    [Serializable]
    public class DuplicateRoleNameException : Exception
    {
        public DuplicateRoleNameException()
        {
        }

        public DuplicateRoleNameException(string message)
            : base(message)
        {
        }

        public DuplicateRoleNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
using System;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class DuplicateScopeException : Exception
    {
        public DuplicateScopeException()
        {
        }

        public DuplicateScopeException(string message)
            : base(message)
        {
        }

        public DuplicateScopeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
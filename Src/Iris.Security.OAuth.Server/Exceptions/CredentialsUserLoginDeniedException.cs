using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class CredentialsUserLoginDeniedException : Exception
    {
        public CredentialsUserLoginDeniedException()
        {
        }

        public CredentialsUserLoginDeniedException(string message)
            : base(message)
        {
        }

        public CredentialsUserLoginDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
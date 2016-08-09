using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class CredentialsUserNotRegisteredException : Exception
    {
        public CredentialsUserNotRegisteredException()
        {
        }

        public CredentialsUserNotRegisteredException(string message)
            : base(message)
        {
        }

        public CredentialsUserNotRegisteredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
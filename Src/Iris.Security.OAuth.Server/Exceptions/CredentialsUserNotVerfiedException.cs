using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class CredentialsUserNotVerfiedException : Exception
    {
        public CredentialsUserNotVerfiedException()
        {
        }

        public CredentialsUserNotVerfiedException(string message)
            : base(message)
        {
        }

        public CredentialsUserNotVerfiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
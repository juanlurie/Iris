using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class CredentialsUserPasswordResetException : Exception
    {
        public CredentialsUserPasswordResetException()
        {
        }

        public CredentialsUserPasswordResetException(string message)
            : base(message)
        {
        }

        public CredentialsUserPasswordResetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
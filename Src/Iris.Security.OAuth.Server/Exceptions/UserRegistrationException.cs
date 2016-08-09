using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message)
            : base(message)
        {
        }
    }
}
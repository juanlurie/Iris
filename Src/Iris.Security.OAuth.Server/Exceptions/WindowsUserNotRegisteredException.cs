using System;
using System.Security.Authentication;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class WindowsUserNotRegisteredException : AuthenticationException
    {
        public WindowsUserNotRegisteredException()
        {
        }

        public WindowsUserNotRegisteredException(string message)
            : base(message)
        {
        }

        public WindowsUserNotRegisteredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
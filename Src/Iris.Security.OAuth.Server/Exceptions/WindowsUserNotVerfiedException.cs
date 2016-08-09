using System;
using System.Security.Authentication;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class WindowsUserNotVerfiedException : AuthenticationException
    {
        public WindowsUserNotVerfiedException()
        {
        }

        public WindowsUserNotVerfiedException(string message)
            : base(message)
        {
        }

        public WindowsUserNotVerfiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
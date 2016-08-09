using System;

namespace Iris.Security.OAuth.Server.Exceptions
{
    [Serializable]
    public class DuplicateOrganizationUnitSiblingNameException : Exception
    {
        public DuplicateOrganizationUnitSiblingNameException()
        {
        }

        public DuplicateOrganizationUnitSiblingNameException(string message)
            : base(message)
        {
        }

        public DuplicateOrganizationUnitSiblingNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
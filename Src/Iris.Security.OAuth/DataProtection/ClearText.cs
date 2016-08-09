// ReSharper disable CheckNamespace

using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin
// ReSharper restore CheckNamespace
{
    public class ClearText : IDataProtector
    {
        public byte[] Protect(byte[] userData)
        {
            return userData;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return protectedData;
        }
    }
}
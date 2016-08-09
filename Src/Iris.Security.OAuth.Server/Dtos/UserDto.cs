using System;

namespace Iris.Security.OAuth.Server.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int FailedLoginCount { get; set; }
        public int FailedPasswordResetCount { get; set; }
        public bool IsAccountVerified { get; set; }
        public bool IsLoginAllowed { get; set; }
        public bool IsWindowsAccount { get; set; }
        public bool RequiresPasswordReset { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLogin { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string OtherPhone { get; set; }
    }
}
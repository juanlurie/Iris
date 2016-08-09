using System;

namespace Iris.Security.OAuth.Server.Dtos
{
    public class AccountabilityDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string AccountableUser { get; set; }
        public string OrganizationalUnit { get; set; }
        public bool IsActive { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
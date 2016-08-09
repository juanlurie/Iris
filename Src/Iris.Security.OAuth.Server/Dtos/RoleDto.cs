using System;

namespace Iris.Security.OAuth.Server.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public Guid CommissionerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
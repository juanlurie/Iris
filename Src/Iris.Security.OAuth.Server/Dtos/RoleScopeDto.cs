using System;

namespace Iris.Security.OAuth.Server.Dtos
{
    public class RoleScopeDto
    {
        public Guid RoleId { get; set; }
        public string ScopeId { get; set; }
    }
}
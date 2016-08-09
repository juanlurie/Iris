using System;
using System.Collections.Generic;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class AddRole : IDomainCommand
    {
        public Guid CommissionerId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Guid Id { get; protected set; }

        public AddRole(string name, string description)
        {
            Id = SequentialGuid.New();
            Name = name;
            Description = description;
        }
    }    
    
    
    public class UpdateRoleScopes : IDomainCommand
    {
        public Guid RoleId { get; protected set; }
        public List<string> ScopeIds { get; set; }

        public UpdateRoleScopes(Guid roleId, List<string> scopeIds)
        {
            RoleId = roleId;
            ScopeIds = scopeIds;
        }
    }
}
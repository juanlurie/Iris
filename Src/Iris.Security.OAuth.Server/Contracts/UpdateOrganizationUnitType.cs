using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class UpdateOrganizationUnitType : IDomainCommand
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateOrganizationUnitType(Guid id, Guid parentId, string name, string description)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Description = description;
        }
    }
}
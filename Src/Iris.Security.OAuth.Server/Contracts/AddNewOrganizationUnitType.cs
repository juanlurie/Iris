using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class AddNewOrganizationUnitType : IDomainCommand
    {
        public Guid ParentId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Guid Id { get; protected set; }

        public AddNewOrganizationUnitType(Guid parentId, string name, string description)
        {
            Id = SequentialGuid.New();
            ParentId = parentId;
            Name = name;
            Description = description;
        }
    }
}
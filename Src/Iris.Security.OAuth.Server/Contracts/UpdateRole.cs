using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class UpdateRole : IDomainCommand
    {
        public Guid Id { get; set; }
        public Guid CommissionerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateRole(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
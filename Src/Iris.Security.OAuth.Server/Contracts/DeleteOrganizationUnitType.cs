using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class DeleteOrganizationUnitType : IDomainCommand
    {
        public Guid Id { get; set; }

        public DeleteOrganizationUnitType(Guid id)
        {
            Id = id;
        }
    }
}
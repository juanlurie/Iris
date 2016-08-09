using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class DeleteRole : IDomainCommand
    {
        public Guid Id { get; set; }

        public DeleteRole(Guid id)
        {
            Id = id;
        }
    }
}
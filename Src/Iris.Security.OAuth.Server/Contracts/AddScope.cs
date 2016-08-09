using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class AddScope : IDomainCommand
    {
        public string Name { get; protected set; }
        public string Value { get; protected set; }
        public Guid Id { get; protected set; }

        public AddScope(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
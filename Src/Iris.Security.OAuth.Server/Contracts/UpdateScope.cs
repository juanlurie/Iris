using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class UpdateScope : IDomainCommand
    {
        public string Name { get; protected set; }
        public string Value { get; protected set; }

        public UpdateScope(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
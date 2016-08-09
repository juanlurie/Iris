using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class DeleteScope : IDomainCommand
    {
        public string Value { get; protected set; }

        public DeleteScope(string value)
        {
            Value = value;
        }
    }
}
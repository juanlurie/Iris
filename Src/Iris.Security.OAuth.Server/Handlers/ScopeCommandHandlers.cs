using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;

namespace Iris.Security.OAuth.Server.Handlers
{
    public class ScopeCommandHandlers : IHandleMessage<AddScope>
                                      , IHandleMessage<UpdateScope>
                                      , IHandleMessage<DeleteScope>
    {
        private readonly ScopeService scopeService;

        public ScopeCommandHandlers(ScopeService scopeService)
        {
            this.scopeService = scopeService;
        }

        public void Handle(AddScope message)
        {
            scopeService.AddScope(message.Name, message.Value);
        }

        public void Handle(UpdateScope message)
        {
            scopeService.UpdateScope(message.Name, message.Value);
        }

        public void Handle(DeleteScope message)
        {
            scopeService.DeleteScope(message.Value);
        }
    }
}
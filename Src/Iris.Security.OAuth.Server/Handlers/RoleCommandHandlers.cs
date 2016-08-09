using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;

namespace Iris.Security.OAuth.Server.Handlers
{
    public class RoleCommandHandlers : IHandleMessage<AddRole>
        , IHandleMessage<UpdateRole>
        , IHandleMessage<DeleteRole>
        , IHandleMessage<UpdateRoleScopes>
    {
        private readonly RoleService roleService;
        private readonly RoleScopeService roleScopeService;

        public RoleCommandHandlers(RoleService roleService, RoleScopeService roleScopeService)
        {
            this.roleService = roleService;
            this.roleScopeService = roleScopeService;
        }

        public void Handle(AddRole message)
        {
            roleService.AddRole(message.Id, message.Name, message.Description);
        }

        public void Handle(UpdateRoleScopes message)
        {
            roleScopeService.ResetRoleScopes(message.RoleId, message.ScopeIds);
        }

        public void Handle(UpdateRole message)
        {
            roleService.UpdateRole(message.Id, message.Name, message.Description);
        }

        public void Handle(DeleteRole message)
        {
            roleService.DeleteRole(message.Id);
        }
    }
}

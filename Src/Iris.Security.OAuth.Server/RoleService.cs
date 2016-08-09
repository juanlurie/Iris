using System;
using System.Data.Entity;
using System.Linq;
using Iris.EntityFramework;
using Iris.Security.OAuth.Server.Controllers;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server
{
    public class RoleService
    {
        private readonly IDbSet<Role> roles;
        private readonly IDbSet<RoleScope> roleScopes;

        public RoleService(IRepositoryFactory repositoryFactory)
        {
            roles = repositoryFactory.GetRepository<Role>();
            roleScopes = repositoryFactory.GetRepository<RoleScope>();
        }

        public void AddRole(Guid id, string name, string description)
        {
            GuardAgainstDuplicateSiblings(id, name);

            roles.Add(new Role
            {
                Id = id,
                Name = name,
                Description = description
            });
        }

        private void GuardAgainstDuplicateSiblings(Guid id, string name)
        {
            var existingRole = roles.Where(x => x.Id != id && x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (existingRole.Any())
            {
                throw new DuplicateRoleNameException(string.Format("There is already another role with the name {0}", name));
            }
        }

        public void UpdateRole(Guid id, string name, string description)
        {
            GuardAgainstDuplicateSiblings(id, name);

            var role = roles.First(x => x.Id == id);

            role.Name = name;
            role.Description = description;
        }

        private void DeleteRoleScopeByRoleId(Guid roleId)
        {
            var roleScopesForRole = roleScopes.Where(x => x.RoleId == roleId).ToList();

            foreach (var roleScope in roleScopesForRole)
            {
                roleScopes.Remove(roleScope);
            }
        }

        public void DeleteRole(Guid id)
        {
            DeleteRoleScopeByRoleId(id);

            var role = roles.SingleOrDefault(x => x.Id == id);
            roles.Remove(role);
        }
    }
}
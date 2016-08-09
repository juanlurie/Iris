using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Iris.EntityFramework;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server
{
    public class RoleScopeService
    {
        private readonly IDbSet<RoleScope> roleScopes;

        public RoleScopeService(IRepositoryFactory repositoryFactory)
        {
            roleScopes = repositoryFactory.GetRepository<RoleScope>();
        }

        public void ResetRoleScopes(Guid id, List<string> scopeIds)
        {
            foreach (var nonDefaultRoleScope in roleScopes.Where(x => x.RoleId == id))
            {
                roleScopes.Remove(nonDefaultRoleScope);
            }

            foreach (var nonDefaultScope in scopeIds)
            {
                roleScopes.Add(new RoleScope()
                {
                    RoleId = id,
                    ScopeValue = nonDefaultScope
                });
            }
        }

        public void AddRoleScope(Guid roleId, string scope)
        {
            roleScopes.Add(new RoleScope
            {
                RoleId = roleId,
                ScopeValue = scope
            });
        }
    }
}
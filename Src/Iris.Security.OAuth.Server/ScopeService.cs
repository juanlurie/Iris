using System;
using System.Data.Entity;
using System.Linq;
using Iris.EntityFramework;
using Iris.Security.OAuth.Server.Controllers;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server
{
    public class ScopeService
    {
        private readonly IDbSet<Scope> scopes;
        private readonly IDbSet<RoleScope> roleScopes;

        public ScopeService(IRepositoryFactory repositoryFactory)
        {
            scopes = repositoryFactory.GetRepository<Scope>();
            roleScopes = repositoryFactory.GetRepository<RoleScope>();
        }

        public void AddScope(string name, string value)
        {
            var existingScopes = GetScopeByValue(value);
            if (existingScopes != null)
            {
                throw new DuplicateScopeException("Scope already exists");
            }

            scopes.Add(new Scope
            {
                Value = value,
                Name = name
            });
        }

        public void DeleteScope(string value)
        {
            var existingScope = GetScopeByValue(value);

            DeleteRoleScopyByValue(value);

            if (existingScope == null)
            {
                return;
            }

            scopes.Remove(existingScope);
        }

        private void DeleteRoleScopyByValue(string value)
        {
            var roleScopesForScope = roleScopes.Where(x => x.ScopeValue == value).ToList();

            foreach (var roleScope in roleScopesForScope)
            {
                roleScopes.Remove(roleScope);
            }
        }

        private Scope GetScopeByValue(string value)
        {
            return
                scopes.FirstOrDefault(
                    scope =>
                        scope.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public void UpdateScope(string name, string value)
        {
            var existingScope = GetScopeByValue(value);
            if (existingScope == null)
            {
                return;
            }

            existingScope.Name = name;
        }
    }
}
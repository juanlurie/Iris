using System;
using System.Collections.Generic;
using System.Linq;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class RoleScopeQueryService
    {
        private readonly IQueryable<RoleScope> roleScopeQuery;

        public RoleScopeQueryService(IDatabaseQuery databaseQuery)
        {
            roleScopeQuery = databaseQuery.GetQueryable<RoleScope>();
        }

        public IEnumerable<RoleScopeDto> GetScopesByRole(Guid roleId)
        {
            return roleScopeQuery.Where(scopes => scopes.RoleId == roleId)
                                           .Select(x => new RoleScopeDto
                                           {
                                               RoleId = x.RoleId,
                                               ScopeId = x.ScopeValue
                                           });
        }

    }
}
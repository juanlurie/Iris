using System;
using System.Collections.Generic;
using System.Linq;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class ScopeQueryService
    {
        private readonly IQueryable<Scope> query;

        public ScopeQueryService(IDatabaseQuery databaseQuery)
        {
            query = databaseQuery.GetQueryable<Scope>();
        }

        public IEnumerable<ScopeDto> GetScopesByValue(string value)
        {
            return query.Where(scopes => scopes.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                        .Select(x=>new ScopeDto
                        {
                            Value = x.Value,
                            Name = x.Name
                        });
        }

        public IEnumerable<ScopeDto> FetchAll()
        {
            return query.Select(x => new ScopeDto
                       {
                           Value = x.Value,
                           Name = x.Name
                       });
        }

        public ScopeDto FetchFirstOrDefault(string value)
        {
            var result = query.FirstOrDefault(x => x.Value.Equals(value));

            if (result == null)
                return null;

            return new ScopeDto
            {
                Value = result.Value,
                Name = result.Name
            };
        }
    }
}
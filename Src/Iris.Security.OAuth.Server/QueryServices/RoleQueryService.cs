using System;
using System.Linq;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class RoleQueryService
    {
        private readonly IQueryable<Role> roleQuery;

        public RoleQueryService(IDatabaseQuery databaseQuery)
        {
            roleQuery = databaseQuery.GetQueryable<Role>();
        }

        public RoleDto[] GetAll()
        {
            return roleQuery.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToArray();
        }

        public RoleDto GetById(Guid id)
        {
            var result = roleQuery.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return null;

            return new RoleDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description
            };
        }
    }
}
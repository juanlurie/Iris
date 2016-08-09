using System;
using System.Data.Entity;
using System.Linq;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class AccountabilityQueryService
    {
        private readonly IQueryable<Accountability> query;

        public AccountabilityQueryService(IDatabaseQuery databaseQuery)
        {
            query = databaseQuery.GetQueryable<Accountability>()
                         .Include(a => a.Role)
                         .Include(a => a.User);
        }

        public AccountabilityDto[] GetTeamMembersWithRole(Guid accountableUserId, Guid teamMemberRoleId)
        {
            var accountabilities = query.Where(a => a.Role.Id == teamMemberRoleId && a.Id != accountableUserId)
                                        .Select(x => new AccountabilityDto
                                        {
                                            Role = x.Role.Name,
                                            AccountableUser = x.User.NameAndSurname(),
                                            End = x.EndOfAccountability,
                                            Start = x.StartOfAccountability,
                                            IsActive = x.IsActive,
                                            Id = x.Id,
                                        })
                                        .ToArray();

            return accountabilities;
        }

        public AccountabilityDto[] GetRolesForUser(Guid accountableUserId)
        {
            var accountabilities =
                 query.Where(a => a.UserId == accountableUserId && a.IsActive && a.EndOfAccountability > DateTime.Now)
                   .Select(x => new AccountabilityDto
                   {
                       Role = x.Role.Name,
                       AccountableUser = x.User.NameAndSurname(),
                       End = x.EndOfAccountability,
                       Start = x.StartOfAccountability,
                       IsActive = x.IsActive,
                       Id = x.Id,
                   })
                    .ToArray();

            return accountabilities;
        }
    }
}
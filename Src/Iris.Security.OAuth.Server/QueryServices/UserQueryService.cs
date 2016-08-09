using System;
using System.Collections.Generic;
using System.Linq;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class UserQueryService
    {
        private readonly IQueryable<User> query;

        public UserQueryService(IDatabaseQuery databaseQuery)
        {
            query = databaseQuery.GetQueryable<User>();
        }

        public IEnumerable<UserDto> FetchAll()
        {
            return query.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.Username,
                FailedLoginCount = x.FailedLoginCount,
                FailedPasswordResetCount = x.FailedPasswordResetCount,
                IsAccountVerified = x.IsAccountVerified,
                IsLoginAllowed = x.IsLoginAllowed,
                IsWindowsAccount = x.IsWindowsAccount,
                RequiresPasswordReset = x.RequiresPasswordReset,
                LastLogin = x.LastLogin,
                LastFailedLogin = x.LastFailedLogin,
                FirstName = x.FirstName,
                Surname = x.Surname,
                Email = x.Email,
                MobilePhone = x.MobilePhone,
            });
        }

        public UserDto GetUserById(Guid userId)
        {
            var result = query.Single(user => user.Id == userId);

            return new UserDto
            {
                Id = result.Id,
                Username = result.Username,
                FailedLoginCount = result.FailedLoginCount,
                FailedPasswordResetCount = result.FailedPasswordResetCount,
                IsAccountVerified = result.IsAccountVerified,
                IsLoginAllowed = result.IsLoginAllowed,
                IsWindowsAccount = result.IsWindowsAccount,
                RequiresPasswordReset = result.RequiresPasswordReset,
                LastLogin = result.LastLogin,
                LastFailedLogin = result.LastFailedLogin,
                FirstName = result.FirstName,
                Surname = result.Surname,
                Email = result.Email,
                MobilePhone = result.MobilePhone,
            };
        }
    }
}

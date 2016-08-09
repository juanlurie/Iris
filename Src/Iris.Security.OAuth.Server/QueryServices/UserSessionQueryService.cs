using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.QueryServices
{
    public class UserSessionQueryService 
    {
        private readonly IQueryable<User> users;

        public UserSessionQueryService(IDatabaseQuery databaseQuery)
        {
            users = databaseQuery.GetQueryable<User>();
        }

        protected IQueryable<User> JoinTables(IQueryable<User> queryable)
        {
            return queryable.Include(user => user.Accountabilities)
                            .Include(user => user.Claims)
                            .Include("Accountabilities.Role.RoleScopes");
        }

        public Claim[] GetUserSession(Guid sessionKey)
        {
            var user = JoinTables(users).SingleOrDefault(u => u.SessionKey == sessionKey);

            if (user == null || !user.IsLoginAllowed || user.RequiresPasswordReset)
            {
                return new Claim[0];
            }

            var userClaims = new List<Claim>();

            userClaims.Add(new Claim(AuthConstants.ClaimTypes.AccountName, string.Format("{0}", user.Username)));
            userClaims.Add(new Claim(AuthConstants.ClaimTypes.UserId, user.Id.ToString()));

            userClaims.AddRange(GetUserClaims(user));
            userClaims.AddRange(GetUserScopes(user));

            return userClaims.ToArray();
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            return user.Claims.Select(claim => new Claim(claim.Type, claim.Value));
        }

        private IEnumerable<Claim> GetUserScopes(User user)
        {
            var scopes = user.Accountabilities.Where(x => x.IsActive && x.EndOfAccountability > DateTime.Now)
                             .SelectMany(a => a.Role.RoleScopes.Select(r => r.ScopeValue))
                             .ToList();

            var claims = scopes.ConvertAll(scope => new Claim(AuthConstants.ClaimTypes.Scope, scope));

            claims.Add(new Claim(AuthConstants.ClaimTypes.Surname, user.Surname));
            claims.Add(new Claim(AuthConstants.ClaimTypes.GivenName, user.FirstName));

            return claims;
        }
    }
}
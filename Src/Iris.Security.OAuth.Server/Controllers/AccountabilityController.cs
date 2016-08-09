using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.QueryServices;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class AccountabilityController : ApiController
    {
        private const string JsonMediaType = "application/json";
        private readonly AccountabilityQueryService accountabilityQueryService;

        public AccountabilityController(AccountabilityQueryService accountabilityQueryService)
        {
            this.accountabilityQueryService = accountabilityQueryService;
        }

        [Route("api/accountability/{id}/team-members/{role}")]
        public HttpResponseMessage GetUsersWithRole(Guid id, Guid role)
        {
            AccountabilityDto[] result = accountabilityQueryService.GetUsersWithRole(id, role);
            return Request.CreateResponse(HttpStatusCode.OK, result, JsonMediaType);
        }

        [Route("api/accountability/{id}/roles/")]
        public HttpResponseMessage GetRolesForUser(Guid id)
        {
            AccountabilityDto[] result = accountabilityQueryService.GetRolesForUser(id);
            return Request.CreateResponse(HttpStatusCode.OK, result, JsonMediaType);
        }
    }
}
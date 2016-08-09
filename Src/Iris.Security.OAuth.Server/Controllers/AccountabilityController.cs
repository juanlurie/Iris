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
        
        [Route("api/accountability/{id}/team-members")]
        public HttpResponseMessage GetTeamMembersWithRole(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, id, JsonMediaType);
        }

        [Route("api/accountability/{id:guid}/team-members/{role:guid}")]
        public HttpResponseMessage GetTeamMembersWithRole(Guid id, Guid role)
        {
            AccountabilityDto[] result = accountabilityQueryService.GetTeamMembersWithRole(id, role);
            return Request.CreateResponse(HttpStatusCode.OK, result, JsonMediaType);
        }

        [Route("api/accountability/{id:guid}/roles/")]
        public HttpResponseMessage GetRolesForUser(Guid id)
        {
            AccountabilityDto[] result = accountabilityQueryService.GetRolesForUser(id);
            return Request.CreateResponse(HttpStatusCode.OK, result, JsonMediaType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.QueryServices;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class ScopeController : ApiController
    {
        private readonly ScopeQueryService scopeQueryService;
        private readonly IInMemoryBus localBus;
        private readonly RoleScopeQueryService roleScopeQueryService;
        private const string JsonMediaType = "application/json";

        public ScopeController(ScopeQueryService scopeQueryService, IInMemoryBus inMemoryBus, RoleScopeQueryService roleScopeQueryService)
        {
            this.scopeQueryService = scopeQueryService;
            this.localBus = inMemoryBus;
            this.roleScopeQueryService = roleScopeQueryService;
        }

        [HttpGet]
        [Route("api/Scope/Role/{id}", Name = "GetScopesForRole")]
        public HttpResponseMessage GetScopesForRole(Guid id)
        {
            var scopeDtos = scopeQueryService.FetchAll();

            foreach (var scopeDto in scopeDtos)
            {
                if (StringConstants.DefaultScopes.Contains(scopeDto.Value))
                {
                    scopeDto.IsDefault = true;
                }
            }

            scopeDtos = scopeDtos.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Value).ToList();

            var roleScopes = roleScopeQueryService.GetScopesByRole(id);

            scopeDtos = scopeDtos.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Value).ToList();

            var results = new
            {
                Scopes = scopeDtos,
                RoleScopes = roleScopes
            };

            return Request.CreateResponse(HttpStatusCode.OK, results, JsonMediaType);
        }

        [HttpPut]
        [Route("api/Scope/Role/{id}", Name = "PutScopesForRole")]
        public HttpResponseMessage PutScopesForRole(Guid id, List<RoleScopeDto> roleScopeDtos)
        {
            var command = new UpdateRoleScopes(id, roleScopeDtos.Select(x => x.ScopeId).ToList());
            localBus.Execute(command);

            return Request.CreateResponse(HttpStatusCode.Accepted, "Scope updated");
        }

        [Route("api/Scope/")]
        public HttpResponseMessage Get()
        {
            var scopeDtos = scopeQueryService.FetchAll();

            foreach (var scopeDto in scopeDtos)
            {
                if (StringConstants.DefaultScopes.Contains(scopeDto.Value))
                {
                    scopeDto.IsDefault = true;
                }
            }

            scopeDtos = scopeDtos.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Value).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, scopeDtos, JsonMediaType);
        }

        [Route("api/Scope/ByValue/{Id}", Name = "GetScope")]
        public HttpResponseMessage GetScope(string value)
        {
            ScopeDto scopeDto = scopeQueryService.FetchFirstOrDefault(value);

            if (scopeDto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, scopeDto, JsonMediaType);
        }

        [Route("api/Scope/ByValue/{scopeValue}", Name = "UpdateScope")]
        public HttpResponseMessage Put(string scopeValue, EditedScope scope)
        {
            try
            {
                var command = new UpdateScope(scope.Name, scope.Value);
                localBus.Execute(command);

                return Request.CreateResponse(HttpStatusCode.Accepted, "Scope updated");
            }
            catch (DuplicateScopeException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [Route("api/Scope/ByValue/{value}", Name = "DeleteScope")]
        public HttpResponseMessage Delete(string value)
        {
            var command = new DeleteScope(value);
            localBus.Execute(command);

            return Request.CreateResponse(HttpStatusCode.OK, "Scope deleted");
        }

        [HttpPost]
        [Route("api/Scope")]
        public HttpResponseMessage Post(NewScope scope)
        {
            try
            {
                var command = new AddScope(scope.Name, scope.Value);
                localBus.Execute(command);

                return Request.CreateLocationResponse(Url.Link("GetScope", new { Id = command.Id }));
            }
            catch (DuplicateScopeException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);
            }

        }
    }
}
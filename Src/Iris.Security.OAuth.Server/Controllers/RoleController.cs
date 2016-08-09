using System;
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
    public class RoleController : ApiController
    {
        private readonly RoleQueryService roleQueryService;
        private readonly IInMemoryBus localBus;
        private const string JsonMediaType = "application/json";

        public RoleController(RoleQueryService roleQueryService,IInMemoryBus inMemoryBus)
        {
            this.roleQueryService = roleQueryService;
            this.localBus = inMemoryBus;
        }

        [Route("api/Role/{Id}", Name = "GetRole")]
        public HttpResponseMessage GetRole(Guid id)
        {
            RoleDto roleDto = roleQueryService.GetById(id);

            if (roleDto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, roleDto, JsonMediaType);
        }

        [Route("api/Role/", Name = "GetRoles")]
        public HttpResponseMessage GetRoles()
        {
            var roleDtos = roleQueryService.GetAll();

            foreach (var roleDto in roleDtos)
            {
                if (StringConstants.DefaultRoles.Contains(roleDto.Name))
                {
                    roleDto.IsDefault = true;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, roleDtos, JsonMediaType);
        }

        [Route("api/Role/{Id}", Name = "UpdateRole")]
        public HttpResponseMessage Put(Guid id, EditedRole role)
        {
            try
            {
                var command = new UpdateRole(id, role.Name, role.Description);
                localBus.Execute(command);

                return Request.CreateResponse(HttpStatusCode.Accepted, "Role updated");
            }
            catch (DuplicateRoleNameException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [Route("api/Role/{Id}", Name = "DeleteRole")]
        public HttpResponseMessage Delete(Guid id)
        {
            var command = new DeleteRole(id);
            localBus.Execute(command);

            return Request.CreateResponse(HttpStatusCode.OK, "Role unit type deleted");
        }

        [HttpPost]
        [Route("api/Role")]
        public HttpResponseMessage Post(NewRole role)
        {
            try
            {
                var command = new AddRole(role.Name, role.Description);
                localBus.Execute(command);

                return Request.CreateLocationResponse(Url.Link("GetRole", new { Id = command.Id }));
            }
            catch (DuplicateRoleNameException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);
            }

        }
    }
}
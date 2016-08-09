using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;
using Iris.Security.OAuth.Server.Dtos;
using Iris.Security.OAuth.Server.QueryServices;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class CredentialsUserRegistration
    {
        public string Username { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }

    [Authorize]
    public class UserController : ApiController
    {
        private const string JsonMediaType = "application/json";
        private readonly IInMemoryBus localBus;
        private readonly UserQueryService userQueryService;

        public UserController(IInMemoryBus localBus, UserQueryService userQueryService)
        {
            this.localBus = localBus;
            this.userQueryService = userQueryService;
        }

        [Route("api/users")]
        public HttpResponseMessage Get()
        {
            var users = userQueryService.FetchAll().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users.ToArray(), JsonMediaType);
        }

        [Route("api/users/{userId}", Name = "GetUser")]
        public HttpResponseMessage GetUser(Guid userId)
        {
            UserDto user = userQueryService.GetUserById(userId);
            return Request.CreateResponse(HttpStatusCode.OK, user, JsonMediaType);
        }

        [Route("api/users/me")]
        public HttpResponseMessage GetClaimDetails()
        {
            var windowsPrincipal = User as WindowsPrincipal;

            if (windowsPrincipal != null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessages.RequireBearerToken);

            return Request.CreateResponse(HttpStatusCode.OK, User.BuildMeResponse(), JsonMediaType);
        }

        [HttpPost]
        [Route("api/users/register/credentials")]
        [OverrideAuthorization]
        public HttpResponseMessage RegisterCredentialsUser(CredentialsUserRegistration input)
        {
            var command = new RegisterCredentialsUser(input.Username, input.GivenName, input.Surname, input.EmailAddress, input.Password);
            localBus.Execute(command);

            return Request.CreateLocationResponse(Url.Link("GetUser", new { command.UserId }));
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Web.Http;
using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;
using Iris.Security.OAuth.Server.QueryServices;
using Iris.Security.OAuth.Services;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizationController : ApiController
    {
        private const string JsonMediaType = "application/json";
        private readonly AccessTokenFactory accessTokenFactory;
        private readonly UserSessionQueryService sessionQueryService;
        private readonly IInMemoryBus localBus;

        public AuthorizationController(AccessTokenFactory accessTokenFactory, UserSessionQueryService sessionQueryService, IInMemoryBus localBus)
        {
            this.accessTokenFactory = accessTokenFactory;
            this.localBus = localBus;
            this.sessionQueryService = sessionQueryService;
        }

        [Route("api/auth/login/credentials")]
        [HttpPost]
        public HttpResponseMessage CredentialsLogin(UserCredentials credentials)
        {
            try
            {
                var command = new LoginCredentialsUser(credentials.Username, credentials.Password);
                localBus.Execute(command);

                var claims = sessionQueryService.GetUserSession(command.SessionKey);
                var token = accessTokenFactory.BuildToken(claims, "credentials");

                return BuildTokenResponse(token);
            }
            catch (AuthenticationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private HttpResponseMessage BuildTokenResponse(object token)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, token, JsonMediaType);

            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoStore = true,
                NoCache = true
            };

            return response;
        }
    }
}
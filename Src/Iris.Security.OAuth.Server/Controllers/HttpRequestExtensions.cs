using System;
using System.Net;
using System.Net.Http;

namespace Iris.Security.OAuth.Server.Controllers
{
    public static class HttpRequestExtensions
    {
        public static HttpResponseMessage CreateLocationResponse(this HttpRequestMessage request, string location)
        {
            var response = request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(location);
            return response;
        }
    }
}
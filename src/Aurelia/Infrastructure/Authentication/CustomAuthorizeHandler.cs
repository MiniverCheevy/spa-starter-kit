using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Fernweh.Core;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Voodoo.Messages;

namespace Fernweh.Infrastructure.Authentication
{
  [Obsolete]
  public class AuthorizationMiddleware
  {
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      var user = IOC.GetCurrentPrincipal();
      var isAuthenticated = IOC.GetCurrentPrincipal().IsAuthenticated;
      var requestPath = context.Request.GetUri().ToString().ToLower();
      if (!isAuthenticated)
        if (requestPath.Contains("api") && !requestPath.Contains("login") && !requestPath.Contains("profile") &&
            !requestPath.Contains("oauth"))
        {
          context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
          context.Response.ContentType = "application/json";

          var response = new Response();
          response.IsOk = false;
          response.Message = "Please login";
          var json = new JavaScriptSerializer().Serialize(response);
          await context.Response.WriteAsync(json).ConfigureAwait(false);
          return;
        }
      await _next(context);
    }
  }
}

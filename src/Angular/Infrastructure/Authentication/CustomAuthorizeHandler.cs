﻿using System;
using System.Net;
using System.Threading.Tasks;
using Core;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Voodoo.Messages;

namespace Web.Infrastructure.Authentication
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

            var requestPath = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request);
            if (!isAuthenticated)
                if (requestPath.Contains("api") && !requestPath.Contains("login") && !requestPath.Contains("profile") &&
                    !requestPath.Contains("oauth"))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = new Response();
                    response.IsOk = false;
                    response.Message = "Please login";
                    var json = JsonConvert.SerializeObject(response);
                    await context.Response.WriteAsync(json).ConfigureAwait(false);
                    return;
                }
            await _next(context);
        }
    }
}
﻿using System.Security.Principal;
using System.Threading.Tasks;
using Core.Operations.CurrentUsers;
using Core.Operations.CurrentUsers.Extras;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Web.Infrastructure.Authentication
{
    public class WindowsAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public WindowsAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.IsSecureRequest())
                if (context.Items[RequestContextProvider.AppPrincipal] == null)
                {
                    var userName = getUserName(context);
                    var userAgent = getUserAgent(context);

                    var response =
                        await new BuildPrincipalCommand(
                            new BuildPrincipalRequest {UserAgent = userAgent, UserName = userName}).ExecuteAsync();
                    if (response.IsOk)
                        context.Items[RequestContextProvider.AppPrincipal] = response.Data;
                }
            await next(context);
        }

        private static string getUserName(HttpContext context)
        {
            var user = context.User;
            var windowsUser = user as WindowsPrincipal;
            var userName = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(windowsUser?.Identity?.Name);
            return userName;
        }

        private string getUserAgent(HttpContext context)
        {
            var key = "User-Agent";
            var headers = context.Request.Headers;
            if (headers.ContainsKey(key))
                return headers[key].ToString();
            return null;
        }
    }
}
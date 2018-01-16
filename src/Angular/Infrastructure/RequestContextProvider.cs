using System;
using Core.Identity;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Web.Infrastructure
{
    public class RequestContextProvider : IRequestContextProvider
    {
        public const string AppPrincipal = "AppPrincipal";
        public const string ClientInfo = "ClientInfo";
        private IHttpContextAccessor httpContextAccessor;

        public Guid Id { get; }

        public RequestContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.Id = Guid.NewGuid();
        }

        public RequestContext RequestContext
        {
            get
            {
                var context = httpContextAccessor.HttpContext;
                var response = new RequestContext
                {
                    AppPrincipal = context.Items[AppPrincipal].To<AppPrincipal>(),
                    ClientInfo = context.Items[ClientInfo].To<ClientInfo>()
                };
                if (context.Request.Headers.ContainsKey("User-Agent"))
                    response.UserAgent = context.Request.Headers["User-Agent"];
                return response;
            }
        }

        private T resolve<T>()
        {
            return httpContextAccessor.HttpContext.RequestServices.GetService(typeof(T)).To<T>();
        }
    }
}
using System.Threading.Tasks;
using Core;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Voodoo;
using Web.Infrastructure.Logging;

namespace Web.Infrastructure
{
    public class CompositionMiddleware
    {
        private readonly RequestDelegate next;

        public CompositionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            IOC.TraceWriter = new TraceWriter();
            IOC.ContextFactory = new ContextFactory();
            IOC.RequestContextProvier = new RequestContextProvider(httpContextAccessor);
            VoodooGlobalConfiguration.ErrorDetailLoggingMethodology = ErrorDetailLoggingMethodology.LogInExceptionData;
            await next(context);
        }
    }
}
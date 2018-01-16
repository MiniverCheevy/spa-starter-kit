using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Infrastructure.ExceptionHandling;

namespace Web.Infrastructure.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private DateTimeOffset start;
        private DateTimeOffset end;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.start = DateTime.UtcNow;
            await next(context);
            this.end = DateTime.UtcNow;
            var duration = end.Subtract(start).Milliseconds;
            var requestLog = new RequestLogFactory(context, duration).GetLog(); 
        }
    }
}
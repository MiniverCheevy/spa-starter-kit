using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Logging;
using Core.Models.Logging;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Web.Infrastructure.Logging
{
    public class HttpContextLogger : ITraceLogger
    {
        public const string TraceLog = "TraceLog";
        private IHttpContextAccessor contextAccessor;
        private HttpContext httpContext => this.contextAccessor.HttpContext;

        public HttpContextLogger(IHttpContextAccessor httpContextAccessor)
        {
            this.contextAccessor = httpContextAccessor;
        }


        public void Log(string log)
        {
            var logs = httpContext.Items[TraceLog].To<List<LogEntry>>();

            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(3)?.GetMethod();

            logs.Add(new LogEntry { CreationDate = DateTime.UtcNow, Log = log, Origin = $"{ methodBase?.ReflectedType?.ToString()}.{ methodBase?.Name}" });
            httpContext.Items[TraceLog] = logs;
        }

        public List<LogEntry> getAllLogs(bool clear = true)
        {
            var logs = httpContext.Items[TraceLog].To<List<LogEntry>>();
            if (clear)
                httpContext.Items[TraceLog] = null;

            return logs;
        }
    }
}

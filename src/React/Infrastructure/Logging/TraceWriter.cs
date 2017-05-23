using Core.Logging;
using Microsoft.ApplicationInsights;
using Voodoo.Logging;

namespace Fernweh.Infrastructure.Logging
{
    public class TraceWriter : ITraceLogger
    {
        public IScopedLogger GetScopedLogger(string scope)
        {
            return new TraceLogger(scope);
        }

        public void Log(string message)
        {
            var telemetry = new TelemetryClient();
            telemetry.TrackTrace(message);

            LogManager.Log(message);
        }
    }
}
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Logging;
using Voodoo.Logging;

namespace Fernweh.Aurelia.Infrastructure.Logging
{
    public class TraceWriter : ITraceLogger
    {
        public IScopedLogger GetScopedLogger(string scope)
        {
            return new TraceLogger(scope);
        }

        public void Log(string message)
        {
            var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetry.TrackTrace(message);

            LogManager.Log(message);
        }
    }
}
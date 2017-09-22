using System;
using System.Collections.Generic;
using System.IO;
using Core.Operations.Errors;
using Core.Operations.Errors.Extras;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Voodoo;
using Voodoo.Logging;

namespace Web.Infrastructure.ExceptionHandling
{
    public class CoreErrorLogger : ILogger
    {
        
        public static IHttpContextAccessor HttpContextAccessor { get; set; }

        public void Log(string message)
        {
            Exception ex = null;
            try
            {
                ex = new Exception(message);
                Log(ex);

            }
            catch (Exception e)
            {
                writeFallbackLog(e);
                writeFallbackLog(ex);
            }
        }

        public void Log(Exception ex)
        {
            try
            {
                var telemetry = new TelemetryClient();
                telemetry.TrackException(ex);

                Console.WriteLine(ex.ToString());
                var context = HttpContextAccessor.HttpContext;
                
                new ErrorAddCommand(new ErrorFactory(ex, context)).ExecuteAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                writeFallbackLog(e);
                writeFallbackLog(ex);
            }
        }

        private void writeFallbackLog(Exception ex)
        {
            try

            {
                var path = Directory.GetCurrentDirectory();
                var logPath = IoNic.PathCombineLocal(path, "logs");
                Console.WriteLine(logPath);
                IoNic.MakeDir(logPath);
                VoodooGlobalConfiguration.LogFilePath = logPath;
                var fallbackLogger = new FallbackLogger();
                fallbackLogger.Log(ex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
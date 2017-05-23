﻿using System;
using System.Collections.Generic;
using System.IO;
using Core.Operations.Errors;
using Core.Operations.Errors.Extras;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Voodoo;
using Voodoo.Logging;

namespace Fernweh.Infrastructure.ExceptionHandling
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
                var context = HttpContextAccessor.HttpContext;
                var error = new ErrorFactory().GetError(ex, context);

                var telemetry = new TelemetryClient();
                var properties = new Dictionary<string, string>();
                telemetry.TrackException(ex, properties);

#pragma warning disable 4014
                new ErrorAddCommand(new ErrorRequest {Error = error}).ExecuteAsync();
#pragma warning restore 4014
            }
            catch (Exception e)
            {
                WriteFallbackLog(e);
                WriteFallbackLog(ex);
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
                var error = new ErrorFactory().GetError(ex, context);
#pragma warning disable 4014
                new ErrorAddCommand(new ErrorRequest {Error = error}).ExecuteAsync();
#pragma warning restore 4014
            }
            catch (Exception e)
            {
                WriteFallbackLog(e);
                WriteFallbackLog(ex);
            }
        }

        private void WriteFallbackLog(Exception ex)
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
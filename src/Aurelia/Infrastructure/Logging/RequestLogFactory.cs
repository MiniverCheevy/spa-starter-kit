using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Core;
using Core.Infrastructure.Logging;
using Core.Models.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UAParser;
using Voodoo;
using Voodoo.Logging;
using Web.Infrastructure.ExceptionHandling;

namespace Web.Infrastructure.Logging
{
  internal class RequestLogFactory : IRequestLogFactory
  {
    private RequestLog log;
    private HttpContext context;

    internal RequestLogFactory(HttpContext context, int duration)
    {
      this.log = new RequestLog() { DurationInMs = duration, CreationDate = DateTime.UtcNow };
      this.context = context;

    }
    public RequestLog GetLog()
    {
      decorateLog();
      return log;
    }

    private void decorateLog()
    {
      populateBrowserInformation();
      log.RequestId = IOC.RequestContextProvier?.RequestContext?.Id;
      log.RequestId = IOC.RequestContext?.Id;
      var logs = IOC.TraceLogger.GetAllLogs(false);
      log.TraceLogs = JsonConvert.SerializeObject(logs);
      log.Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
      log.Host = System.Environment.MachineName;
      log.Name = context.Request.Path;

      var formatInfo = DateTimeFormatInfo.CurrentInfo;
      var calander = formatInfo.Calendar;

      log.WeekOfYear = calander.GetWeekOfYear(log.CreationDate.Date, formatInfo.CalendarWeekRule, formatInfo.FirstDayOfWeek);
      log.DayOfYear = calander.GetDayOfYear(log.CreationDate.Date);

    }

    private void populateBrowserInformation()
    {
      var browser = getBrowserInformation();
      if (browser == null)
        return;

      var agent = browser.UserAgent;
      log.BrowserFamily = agent.Family;
      log.BrowserVersion = $"{agent.Major}.{agent.Minor}";
      log.OSFamily = browser.OS.Family;
    }
    private ClientInfo getBrowserInformation()
    {
      if (!context.Request.Headers.ContainsKey("User-Agent"))
        return null;
      var userAgent = context.Request.Headers["User-Agent"];

      if (string.IsNullOrWhiteSpace(userAgent))
        return null;
      log.UserAgent = userAgent;

      try
      {
        var clientInfo = Parser.GetDefault().Parse(userAgent);
        return clientInfo;
      }
      catch (Exception ex)
      {
        new CoreErrorLogger().Log(ex);
      }
      return null;
    }
  }
}
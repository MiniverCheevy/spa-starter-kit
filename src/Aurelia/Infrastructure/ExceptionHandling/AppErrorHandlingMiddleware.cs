using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Voodoo;
using Voodoo.Messages;

namespace Fernweh.Infrastructure.ExceptionHandling
{
  public class AppErrorHandlingMiddleware
  {
    private readonly RequestDelegate next;

    public AppErrorHandlingMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context, IHttpContextAccessor httpContextAccessor)
    {
      context.Request.EnableRewind();
      CoreErrorLogger.HttpContextAccessor = httpContextAccessor;
      VoodooGlobalConfiguration.RegisterLogger(new CoreErrorLogger());
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        await handleExceptionAsync(context, ex);
      }
    }

    private async Task handleExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";

      if (exception != null)
      {
        new CoreErrorLogger().Log(exception);
        var ex = exception;
        var response = new Response();
        while (ex.InnerException != null)
          ex = ex.InnerException;
        response.Message = ex.Message;
        response.IsOk = false;
        var json = JsonConvert.SerializeObject(response);
        context.Response.ContentType = "application/json";
        Console.WriteLine(exception.ToString());
        await context.Response.WriteAsync(json).ConfigureAwait(false);
      }
    }
  }
}

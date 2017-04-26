using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fernweh.Aurelia.Infrastructure.ExceptionHandling;
using Fernweh.Aurelia.Infrastructure.Logging;
using Fernweh.Aurelia.Infrastructure.Settings;
using Fernweh.Core;
using Fernweh.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Aurelia.Infrastructure
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

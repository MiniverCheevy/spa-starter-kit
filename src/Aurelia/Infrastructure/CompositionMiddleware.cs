using System.Threading.Tasks;
using Fernweh.Core;
using Fernweh.Core.Infrastructure;
using Fernweh.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Infrastructure
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

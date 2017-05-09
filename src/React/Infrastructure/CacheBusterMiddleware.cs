using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace React.Infrastructure
{
    //TODO: ideally this should only add headers to the index.html
    //and webpack should be configured to js/css files so they'll be 
    //unique on the first load and cached after that
  public class CacheBusterMiddleware
  {
    private readonly RequestDelegate next;
    public CacheBusterMiddleware(RequestDelegate next)
    {
      this.next = next;
    }
    public async Task Invoke(HttpContext context)
    {
      var extensions = new string[] { ".js", ".html", ".css", @"/" };
      var requestPath = context.Request.Path.ToString().ToLower();
      if (extensions.Any(c => requestPath.EndsWith(c)))
      {
        context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
        context.Response.Headers.Append("Pragma", "no-cache");
        context.Response.Headers.Append("Expires", "0");
        context.Response.Headers.Append("X-UA-Compatible", "IE=Edge");
      }
      await next(context);
    }
  }
}

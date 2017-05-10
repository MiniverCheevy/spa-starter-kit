using System.Security.Principal;
using System.Threading.Tasks;
using Fernweh.Core.Identity;
using Fernweh.Core.Operations.CurrentUsers;
using Fernweh.Core.Operations.CurrentUsers.Extras;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Infrastructure.Authentication
{
  public class WindowsAuthenticationMiddleware
  {
    private readonly RequestDelegate next;

    public WindowsAuthenticationMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      if (context.Items[RequestContextProvider.AppPrincipal] == null)
      {
        var principal = AppPrincipal.GetAnonymousPrincipal();
        var userName = getUserName(context);
        var userAgent = getUserAgent(context);

        var response =
          await new BuildPrincipalCommand(new BuildPrincipalRequest {UserAgent = userAgent, UserName = userName})
            .ExecuteAsync();
        if (response.IsOk)
          context.Items[RequestContextProvider.AppPrincipal] = response.Data;
      }
      await next(context);
    }

    private static string getUserName(HttpContext context)
    {
      var user = context.User;
      var windowsUser = user as WindowsPrincipal;
      var userName = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(windowsUser?.Identity?.Name);
      return userName;
    }

    private string getUserAgent(HttpContext context)
    {
      var key = "User-Agent";
      var headers = context.Request.Headers;
      if (headers.ContainsKey(key))
        return headers[key].ToString();
      return null;
    }
  }
}

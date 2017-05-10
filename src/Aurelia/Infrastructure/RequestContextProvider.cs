using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Infrastructure
{
  public class RequestContextProvider : IRequestContextProvider
  {
    public const string AppPrincipal = "AppPrincipal";
    public const string ClientInfo = "ClientInfo";
    private IHttpContextAccessor httpContextAccessor;

    public RequestContextProvider(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
    }

    public RequestContext RequestContext
    {
      get
      {
        var response = new RequestContext();
        response.AppPrincipal = httpContextAccessor.HttpContext.Items[AppPrincipal].To<AppPrincipal>();
        response.ClientInfo = httpContextAccessor.HttpContext.Items[ClientInfo].To<ClientInfo>();
        return response;
      }
    }

    private T Resolve<T>()
    {
      return httpContextAccessor.HttpContext.RequestServices.GetService(typeof(T)).To<T>();
    }
  }
}

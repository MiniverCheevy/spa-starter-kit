using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Security;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Aurelia.Infrastructure
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

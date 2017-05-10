using Microsoft.AspNetCore.Http;
using Voodoo;

namespace Fernweh.Infrastructure
{
  public static class ContextExtensions
  {
    public static bool IsSecureRequest(this HttpContext context)
    {
      return context.Request.Path.To<string>().ToLower().Contains(@"/api/");
    }
  }
}

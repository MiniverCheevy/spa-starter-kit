using Core.Context;
using Core.Identity;
using Core.Infrastructure;
using Core.Logging;
using Voodoo;

namespace Core
{
    public static class IOC
    {
        public static Settings Settings { get; set; }
        public static IRequestContextProvider RequestContextProvier { get; set; }

        public static IContextFactory ContextFactory { get; set; }
        public static ITraceLogger TraceWriter { get; set; }
        public static RequestContext RequestContext => RequestContextProvier?.RequestContext;

        public static AppPrincipal GetCurrentPrincipal()
        {
            var result = RequestContext?.AppPrincipal;
            result.ThrowIfNull("Principal Not Found");
            return result;
        }

        public static MainContext GetContext()
        {
            return ContextFactory.GetContext();
        }

        public static string GetConnectionString()
        {
            return ContextFactory?.GetConnectionString() ?? Settings.DefaultConnectionString;
        }
    }
}
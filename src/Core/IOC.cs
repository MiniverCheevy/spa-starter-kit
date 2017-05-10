using Fernweh.Core.Context;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Logging;
using Voodoo;

namespace Fernweh.Core
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

        public static FernwehContext GetContext()
        {
            return ContextFactory.GetContext();
        }

        public static string GetConnectionString()
        {
            return ContextFactory.GetConnectionString();
        }
    }
}
using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public class ContextFactory : IContextFactory
    {
        public AppContext GetContext()
        {
            return new AppContext(IOC.Settings.DefaultConnectionString);
        }

        public string GetConnectionString()
        {
            return IOC.Settings.DefaultConnectionString;
        }
    }
}
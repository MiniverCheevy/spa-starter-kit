using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public class ContextFactory : IContextFactory
    {
        public MainContext GetContext()
        {
            return new MainContext(IOC.Settings.DefaultConnectionString);
        }

        public string GetConnectionString()
        {
            return IOC.Settings.DefaultConnectionString;
        }
    }
}
using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public class ContextFactory : IContextFactory
    {
        public FernwehContext GetContext()
        {
            return new FernwehContext(IOC.Settings.DefaultConnectionString);
        }

        public string GetConnectionString()
        {
            return IOC.Settings.DefaultConnectionString;
        }
    }
}
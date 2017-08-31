using Core.Context;

namespace Core.Infrastructure
{
    public class ContextFactory : IContextFactory
    {
        public DatabaseContext GetContext()
        {
            return new DatabaseContext(IOC.Settings.DefaultConnectionString);
        }

        public string GetConnectionString()
        {
            return IOC.Settings.DefaultConnectionString;
        }
    }
}
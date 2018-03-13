using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure
{
    public class ContextFactory : IContextFactory
    {
        public DatabaseContext GetContext()
        {

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());

            return new DatabaseContext(optionsBuilder.Options);
        }

        public string GetConnectionString()
        {
            return IOC.Settings.DefaultConnectionString;
        }
    }
}
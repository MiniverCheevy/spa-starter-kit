using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public interface IContextFactory
    {
        MainContext GetContext();

        string GetConnectionString();
    }
}
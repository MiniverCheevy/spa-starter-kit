using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public interface IContextFactory
    {
        AppContext GetContext();

        string GetConnectionString();
    }
}
using Fernweh.Core.Context;

namespace Fernweh.Core.Infrastructure
{
    public interface IContextFactory
    {
        FernwehContext GetContext();

        string GetConnectionString();
    }
}
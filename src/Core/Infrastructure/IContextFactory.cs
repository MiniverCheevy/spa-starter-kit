using Core.Context;

namespace Core.Infrastructure
{
    public interface IContextFactory
    {
        MainContext GetContext();

        string GetConnectionString();
    }
}
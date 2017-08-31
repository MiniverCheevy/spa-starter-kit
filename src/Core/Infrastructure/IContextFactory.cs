using Core.Context;

namespace Core.Infrastructure
{
    public interface IContextFactory
    {
        DatabaseContext GetContext();

        string GetConnectionString();
    }
}
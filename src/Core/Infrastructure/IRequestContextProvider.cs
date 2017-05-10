namespace Fernweh.Core.Infrastructure
{
    public interface IRequestContextProvider
    {
        RequestContext RequestContext { get; }
    }
}
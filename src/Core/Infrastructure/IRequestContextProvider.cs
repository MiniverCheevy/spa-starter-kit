namespace Core.Infrastructure
{
    public interface IRequestContextProvider
    {
        RequestContext RequestContext { get; }
    }
}
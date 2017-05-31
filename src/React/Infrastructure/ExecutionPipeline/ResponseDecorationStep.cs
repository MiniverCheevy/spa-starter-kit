using System.Threading.Tasks;
using Fernweh.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace Fernweh.Infrastructure.ExecutionPipeline
{
    public class ResponseDecorationStep<TRequest, TResponse> : Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task<ExecutionState<TRequest, TResponse>> processAsync()
        {
            
            return Task.FromResult(state);
        }
    }
}
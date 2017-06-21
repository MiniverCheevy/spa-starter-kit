using System.Threading.Tasks;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline.Models;

namespace Web.Infrastructure.ExecutionPipeline
{
    public class ResponseDecorationStep<TRequest, TResponse> : Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task<ExecutionState<TRequest, TResponse>> processAsync()
        {
            if (!state.Response.IsOk && state.Context.Response.StatusCode == 200)
            {
                state.Context.Response.StatusCode = 500;
                return Task.FromResult(state);
            }
            return Task.FromResult(state);
        }
    }
}
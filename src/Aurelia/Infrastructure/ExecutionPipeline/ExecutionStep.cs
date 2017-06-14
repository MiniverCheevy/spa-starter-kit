using System.Threading.Tasks;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline.Models;

namespace Web.Infrastructure.ExecutionPipeline
{
    internal class ExecutionStep<TRequest, TResponse> : Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override async Task<ExecutionState<TRequest, TResponse>> processAsync()
        {
            state.Response = await state.Command.ExecuteAsync();
            return state;
        }
    }
}
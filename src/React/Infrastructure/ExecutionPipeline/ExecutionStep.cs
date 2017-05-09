using System.Threading.Tasks;
using React.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline
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
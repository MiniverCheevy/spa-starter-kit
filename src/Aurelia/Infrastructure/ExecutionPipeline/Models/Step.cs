using System.Threading.Tasks;
using Voodoo.Messages;

namespace Fernweh.Infrastructure.ExecutionPipeline.Models
{
    public abstract class Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected ExecutionState<TRequest, TResponse> state;

        public async Task<ExecutionState<TRequest, TResponse>> ExecuteAsync(
            ExecutionState<TRequest, TResponse> executionRequest)
        {
            state = executionRequest;
            return await processAsync();
        }

        protected abstract Task<ExecutionState<TRequest, TResponse>> processAsync();
    }
}
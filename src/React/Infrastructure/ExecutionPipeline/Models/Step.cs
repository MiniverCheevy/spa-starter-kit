using System.Threading.Tasks;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline.Models
{
    public abstract class Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected ExecutionState<TRequest, TResponse> request;

        public async Task<ExecutionState<TRequest, TResponse>> ExecuteAsync(ExecutionState<TRequest, TResponse> executionRequest)
        {
            this.request = executionRequest;
            await process();
            return await Task.FromResult(request);

        }
        protected abstract Task process();  

    }
}
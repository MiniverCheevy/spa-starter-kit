using System.Threading.Tasks;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline.Models;

namespace Web.Infrastructure.ExecutionPipeline
{
    internal class ModelStateVerificationStep<TRequest, TResponse> : Step<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task<ExecutionState<TRequest, TResponse>> processAsync()
        {
            if (state.Request == null && state.ModelState.ErrorCount > 0)
            {
                var response = new TResponse {IsOk = false, Message = "Model binding error"};
                foreach (var error in state.ModelState.Keys)
                {
                    var value = state.ModelState[error];
                    foreach (var errorValue in value.Errors)
                        response.Details.Add(new NameValuePair(error, errorValue.ErrorMessage));
                }
                state.Response = response;
                state.IsDone = true;
            }
            return Task.FromResult(state);
        }
    }
}
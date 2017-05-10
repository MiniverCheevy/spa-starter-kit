using System.Threading.Tasks;
using Fernweh.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace Fernweh.Infrastructure.ExecutionPipeline
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

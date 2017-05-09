using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using React.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline
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
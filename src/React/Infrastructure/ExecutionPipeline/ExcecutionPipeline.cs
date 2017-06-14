using System.Collections.Generic;
using System.Threading.Tasks;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline.Models;

namespace Web.Infrastructure.ExecutionPipeline
{
    public delegate Task ExecutionDelegate<TRequest, TResponse>
        (ExecutionState<TRequest, TResponse> context)
        where TResponse : class, IResponse, new()
        where TRequest : class;


    //TODO: should I remove web api entirely and switch this to middleware
    //that would instantiate the command and hydrate the response directly 
    //from the context 
    public class ExcecutionPipeline<TRequest, TResponse>
        where TResponse : class, IResponse, new()
        where TRequest : class
    {
        private ExecutionState<TRequest, TResponse> state;

        private List<Step<TRequest, TResponse>> steps = new List<Step<TRequest, TResponse>>
            {
                new ModelStateVerificationStep<TRequest, TResponse>(),
                new AuthorizationStep<TRequest, TResponse>(),
                new ExecutionStep<TRequest, TResponse>()
            }
            ;

        public ExcecutionPipeline(ExecutionState<TRequest, TResponse> executionState)
        {
            state = executionState;
        }

        public async Task<TResponse> ExecuteAsync()
        {
            foreach (var step in steps)
            {
                state = await step.ExecuteAsync(state);
                if (state.IsDone)
                    break;
            }
            state = await new ResponseDecorationStep<TRequest, TResponse>().ExecuteAsync(state);
            return state.Response;
        }
    }
}
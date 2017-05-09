using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using React.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline
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
        private List<Step<TRequest, TResponse>> steps = new List<Step<TRequest, TResponse>>()
        {
            new ModelStateVerificationStep<TRequest,TResponse>(),
            new AuthorizationStep<TRequest,TResponse>(),
            new ExecutionStep<TRequest,TResponse>(),            
           
        }
            ;
        private ExecutionState<TRequest, TResponse> state;

        public ExcecutionPipeline(ExecutionState<TRequest, TResponse> executionState)
        {
            this.state = executionState;
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

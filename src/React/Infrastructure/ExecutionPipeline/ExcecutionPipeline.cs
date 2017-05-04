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

    public class ExcecutionPipeline<TRequest, TResponse>
         where TResponse : class, IResponse, new()
        where TRequest : class
    {
        private List<Step<TRequest, TResponse>> steps = new List<Step<TRequest, TResponse>>()
        {
            new ModelStateVerificationStep(),
            new AuthorizationStep<,>(),
            new ExecutionStep(),
            new ResponseDecorationStep()
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
                result = await step.Execute(executionRequest);
                if (result.IsDone)
                    break;
                else
                    executionRequest = result.Request;
            }
            new ResponseDecorationStep().
            return result.Resposne;
        }
    }
}

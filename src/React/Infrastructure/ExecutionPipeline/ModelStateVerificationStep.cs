using System;
using System.Threading.Tasks;
using React.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline
{
    internal class ModelStateVerificationStep<TRequest, TResponse> : Step<TRequest, TResponse>
         where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task process()
        {
            throw new NotImplementedException();
        }
    }
}
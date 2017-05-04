using System.Threading.Tasks;
using React.Infrastructure.ExecutionPipeline.Models;
using Voodoo.Messages;

namespace React.Infrastructure.ExecutionPipeline
{
    internal class AuthorizationStep<TRequest, TResponse> : Step<TRequest, TResponse>
         where TResponse : class, IResponse, new()
        where TRequest : class
    {
        protected override Task process()
        {
            throw new System.NotImplementedException();
        }
    }
}
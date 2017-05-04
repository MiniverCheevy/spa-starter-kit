using System.Security;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace React.Infrastructure.ExecutionPipeline.Models
{
    public class ExecutionState<TRequest,TResponse> where TResponse : class, IResponse, new() where TRequest : class
    {
        public ModelState ModelState { get; set; }
        public ExecutorAsync<TRequest,TResponse> Executor { get; set; }
        public SecurityContext SecurityContext { get; set; }
        public bool IsDone { get; set; }
        public TResponse Response { get; set; }
        public HttpContext Context { get; set; }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Fernweh.Infrastructure.ExecutionPipeline.Models
{
  public class ExecutionState<TRequest, TResponse> where TResponse : class, IResponse, new() where TRequest : class
  {
    public ModelStateDictionary ModelState { get; set; }
    public SecurityContext SecurityContext { get; set; }
    public bool IsDone { get; set; }
    public TResponse Response { get; set; }
    public TRequest Request { get; set; }
    public ExecutorAsync<TRequest, TResponse> Command { get; set; }
    public HttpContext Context { get; set; }
  }
}

using System;
using System.Security;
using System.Threading.Tasks;
using Core.Models.Exceptions;
using Core.Operations.Errors.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Core.Operations.Errors
{
    [Rest(Verb.Post, RestResources.MobileError, true)]
    public class MobileErrorAddCommand : CommandAsync<MobileErrorRequest, Response>
    {
        public MobileErrorAddCommand(MobileErrorRequest request) : base(request)
        {
        }

        protected override async Task<Response> ProcessRequestAsync()
        {
            using (var context = IOC.GetContext())
            {
                request.ErrorMsg = SecurityElement.Escape(request.ErrorMsg);
                request.ErrorObject = SecurityElement.Escape(request.ErrorObject);

                var error = new Error();
                error.User = IOC.GetCurrentPrincipal().UserName;
                error.Type = "mobile";
                error.Host = request.Url;
                error.Message = request.ErrorMsg;
                error.CreationDate = DateTime.UtcNow;
                error.GUID = Guid.NewGuid();
                error.FullJson =
                    $"{{\"CustomData\":{{ host:\"{request.Url}\" type:\"javascript\" message:\"{request.ErrorMsg}\" detail:\"{request.ErrorObject}\" user:\"{error.User}\" time:\"{DateTime.UtcNow:o}\" }}}}";
                context.Errors.Add(error);
                await context.SaveChangesAsync();
            }
            return response;
        }
    }
}
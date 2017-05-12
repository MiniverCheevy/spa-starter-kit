using System.Threading.Tasks;
using Fernweh.Core.Identity;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Fernweh.Core.Operations.CurrentUsers
{
    [Rest(Verb.Get, RestResources.CurrentUser, AllowAnonymous = true)]
    public class GetCurrentUserCommand : QueryAsync<EmptyRequest, Response<AppPrincipal>>
    {
        public GetCurrentUserCommand(EmptyRequest request) : base(request)
        {
        }

        protected override Task<Response<AppPrincipal>> ProcessRequestAsync()
        {
            var user = IOC.RequestContext.AppPrincipal ?? 
                new AppPrincipal { UserName = "anonymous" };
            response.Data = user;
            return Task.FromResult(response);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Security;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Fernweh.Core.Operations.CurrentUsers
{    
    [Rest(Verb.Get, RestResources.CurrentUser, AllowAnonymous =true)]
    public class GetCurrentUserCommand : QueryAsync<EmptyRequest, Response<AppPrincipal>>
    {
        public GetCurrentUserCommand(EmptyRequest request) : base(request)
        {
        }

        protected override Task<Response<AppPrincipal>> ProcessRequestAsync()
        {
            response.Data = IOC.RequestContext.AppPrincipal;
            return Task.FromResult(response);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Infrastructure;
using Voodoo.Validation.Infrastructure;
using System.Data.Entity;
using Fernweh.Core;
using Fernweh.Core.Context;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Models;
using Fernweh.Core.Models.Identity;
using Voodoo;
using Fernweh.Core.Operations.Users.Extras;
using Fernweh.Core.Security;

namespace Fernweh.Core.Operations.Users
{
    [Rest(Verb.Get, RestResources.UserList)]
    public class UserListQuery : QueryAsync<UserQueryRequest, UserQueryResponse>
    {
        private FernwehContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        private bool isAdmin;
        private IQueryable<User> query;
        private AppPrincipal currentUser;

        public UserListQuery(UserQueryRequest request) : base(request)
        {
        }

        protected override async Task<UserQueryResponse> ProcessRequestAsync()
        {
            currentUser = IOC.GetCurrentPrincipal();
            isAdmin = currentUser.IsAdmin;

            if (!isAdmin)
                return response;

            using (context = IOC.GetContext())
            {
                query = context.UserRepository().GetUserAndRolesQuery();

                await buildList();

                return response;
            }
        }

        private async Task buildList()
        {
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                query = query.Where(c => c.FirstName.Contains(request.SearchText)
                                         || c.LastName.Contains(request.SearchText)
                                         || c.UserName.Contains(request.SearchText)
                                         || c.Roles.Any(r => r.Name.Contains(request.SearchText)
                                         ));
            }
            var data = await query.ToPagedResponseAsync(request);
            response.From(data, c => c.ToUserMessage());
        }
    }
}
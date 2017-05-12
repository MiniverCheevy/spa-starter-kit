using System.Linq;
using System.Threading.Tasks;
using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;
using Fernweh.Core.Operations.Users.Extras;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Fernweh.Core.Operations.Users
{
    [Rest(Verb.Get, RestResources.UserList, Roles = new[] {RoleNames.Administrator})]
    public class UserListQuery : QueryAsync<UserQueryRequest, UserQueryResponse>
    {
        private AppContext context;
        private IQueryable<User> query;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public UserListQuery(UserQueryRequest request) : base(request)
        {
        }

        protected override async Task<UserQueryResponse> ProcessRequestAsync()
        {
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
                query = query.Where(c => c.FirstName.Contains(request.SearchText)
                                         || c.LastName.Contains(request.SearchText)
                                         || c.UserName.Contains(request.SearchText)
                                         || c.Roles.Any(r => r.Name.Contains(request.SearchText)
                                         ));
            var data = await query.ToPagedResponseAsync(request);
            response.From(data, c => c.ToUserMessage());
        }
    }
}
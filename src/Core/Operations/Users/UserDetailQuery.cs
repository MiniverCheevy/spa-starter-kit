using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Models.Identity;
using Core.Models.Mappings;
using Core.Operations.Users.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Core.Operations.Users
{
    

    [Rest(Verb.Get, RestResources.User, Roles = new[] {RoleNames.Administrator})]
    public class UserDetailQuery : QueryAsync<IdRequest, Response<UserDetail>>
    {
        private DatabaseContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public UserDetailQuery(IdRequest request) : base(request)
        {
        }

        protected override async Task<Response<UserDetail>> ProcessRequestAsync()
        {
            var model = new User();
            if (request.Id != 0)
                using (context = IOC.GetContext())
                {
                    var query = context.Users.Include(c => c.Roles).AsNoTracking().AsQueryable();
                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                        throw new Exception(UserMessages.NotFound);
                }
            response.Data = model.ToUserDetail();
            return response;
        }
    }
}
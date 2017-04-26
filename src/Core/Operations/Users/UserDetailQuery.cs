using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fernweh.Core.Context;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Models.Identity;
using Fernweh.Core.Operations.Users.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Fernweh.Core.Operations.Users
{
    [Rest(Verb.Get, RestResources.UserDetail)]
    public class UserDetailQuery : QueryAsync<IdRequest, Response<UserDetail>>
    {
        private FernwehContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public UserDetailQuery(IdRequest request) : base(request)
        {
        }

        protected override async Task<Response<UserDetail>> ProcessRequestAsync()
        {
            var model = new User();
            var principal = IOC.GetCurrentPrincipal();
            if (principal == null)
                throw new Exception("Principal not found exception.");
            if (request.Id != 0)
            {
                using (context = IOC.GetContext())
                {
                    var query = context.Users.Include(c => c.Roles).AsNoTracking().AsQueryable();

                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                        throw new Exception(UserMessages.NotFound);
                }
            }
            response.Data = model.ToUserDetail();
            return response;
        }
    }
}
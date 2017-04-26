using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Fernweh.Core.Context;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Operations.Users.Extras;
using Fernweh.Core.Security;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Fernweh.Core.Operations.Users
{
    [Rest(Verb.Delete, RestResources.UserDetail)]
    public class UserDeleteCommand : CommandAsync<IdRequest, Response>
    {
        protected FernwehContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        private AppPrincipal currentUser;

        public UserDeleteCommand(IdRequest request) : base(request)
        {
        }

        protected override async Task<Response> ProcessRequestAsync()
        {
            currentUser = IOC.GetCurrentPrincipal();
            using (context = IOC.GetContext())
            {
                var model = await context.Users.Include(x => x.Roles)
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (model == null)
                    throw new Exception(UserMessages.NotFound);

                if (model.Id == currentUser.UserId)
                {
                    throw new LogicException("You cannot delete yourself!");
                }
                context.Users.Remove(model);
                response.NumberOfRowsEffected = await context.SaveChangesAsync();
            }
            response.Message = UserMessages.DeleteOk;
            return response;
        }
    }
}
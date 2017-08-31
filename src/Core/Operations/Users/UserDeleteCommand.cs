using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Core.Context;
using Core.Identity;
using Core.Operations.Users.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Core.Operations.Users
{
    [Rest(Verb.Delete, RestResources.User, Roles = new[] {RoleNames.Administrator})]
    public class UserDeleteCommand : CommandAsync<IdRequest, Response>
    {
        protected DatabaseContext context;
        private AppPrincipal currentUser;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

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
                    throw new LogicException("You cannot delete yourself!");
                context.Users.Remove(model);
                response.NumberOfRowsEffected = await context.SaveChangesAsync();
            }
            response.Message = UserMessages.DeleteOk;
            return response;
        }
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Identity;
using Core.Models.Identity;
using Core.Operations.Lists;
using Core.Operations.Users.Extras;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Core.Operations.Users
{
    [Rest(Verb.Put, RestResources.UserDetail, Roles = new[] {RoleNames.Administrator})]
    public class UserSaveCommand : CommandAsync<UserDetail, NewItemResponse>
    {
        private List<Role> allRoles;
        protected MainContext context;
        protected bool isNew;
        private User model;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public UserSaveCommand(UserDetail request) : base(request)
        {
        }

        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //request.<Something>.Validate();

            using (context = IOC.GetContext())
            {
                model = await createOrGetExisting();
                model.ThrowIfNull(UserMessages.NotFound);
                allRoles = context.Roles.ToList();
                updateModel();
                generatePasswordIfNeeded();

                await context.SaveChangesAsync();

                response.NewItemId = model.Id;
            }

            response.Message = isNew ? UserMessages.AddOk : UserMessages.UpdateOk;

            return response;
        }

        private void updateModel()
        {
            model.UpdateFrom(request);
            context.Sync(model.Roles, request.Roles, c => c.Id, c => c.Value, mapRole);
        }

        private Role mapRole(IListItem clientRole, Role dbRole)
        {
            return allRoles.FirstOrDefault(c => c.Id == clientRole.Value);
        }


        private void generatePasswordIfNeeded()
        {
            if (string.IsNullOrWhiteSpace(request.Password))
                return;

            if (request.Password.Trim() != request.ConfirmPassword.Trim())
                throw new LogicException("Password and Confirm Password do not match!");
            var manager = new PasswordManager();
            manager.BuildPasswordAndSalt(ref model, request.Password);
        }

        protected async Task<User> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew = true;
                var entity = new User();
                context.Users.Add(entity);
                return entity;
            }
            return await context.Users
                .Include(c => c.Roles)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
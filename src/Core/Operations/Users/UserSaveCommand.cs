using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;
using System.Data.Entity;
using Fernweh.Core;
using Fernweh.Core.Context;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;
using Fernweh.Core.Models.Identity;
using Fernweh.Core.Operations.Lists;
using Fernweh.Core.Operations.Roles.Extras;
using Voodoo.Infrastructure;
using Fernweh.Core.Operations.Users.Extras;
using Fernweh.Core.Security;

namespace Fernweh.Core.Operations.Users
{
    [Rest(Verb.Put, RestResources.UserDetail,
        Roles = new string[] {RoleNames.Administrator})]
    public class UserSaveCommand : CommandAsync<UserDetail, NewItemResponse>
    {
        protected bool isNew;
        protected FernwehContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        private User model;
        private List<Role> allRoles;
        private AppPrincipal user;

        public UserSaveCommand(UserDetail request) : base(request)
        {
        }

        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //request.<Something>.Validate();
            this.user = IOC.GetCurrentPrincipal();
            using (context = IOC.GetContext())
            {
                model = await createOrGetExisting();
                allRoles = context.Roles.ToList();
                if (model == null)
                    throw new Exception(UserMessages.NotFound);

                updateModel();
                generatePasswordIfNeeded();
                await context.SaveChangesAsync();

                response.NewItemId = model.Id;
            }

            response.Message = isNew
                ? UserMessages.AddOk
                : UserMessages.UpdateOk;

            return response;
        }

        private void updateModel()
        {
            model.UpdateFrom(request);
            synchronizeRoles();
        }

        private void synchronizeRoles()
        {
            var client = request.Roles;
            var target = model.Roles;
            context.Sync(target, client, c => c.Id, c => c.Value, getRoles);
        }

        private Role getRoles(IListItem arg1, Role arg2)
        {
            return allRoles.FirstOrDefault(c => c.Id == arg1.Value);
        }


        private void generatePasswordIfNeeded()
        {
            if (string.IsNullOrWhiteSpace(request.Password))
                return;

            if (request.Password.Trim() != request.ConfirmPassword.Trim())
            {
                throw new LogicException("Password and Confirm Password do not match!");
            }
            var manager = new PasswordManager();
            manager.BuildPasswordAndSalt(ref model, request.Password);
        }

        protected async Task<User> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew = true;
                var model = new User();
                context.Users.Add(model);
                return model;
            }
            else
            {
                return await context.Users
                    .Include(c => c.Roles)
                    .FirstOrDefaultAsync(c => c.Id == request.Id);
            }
        }
    }
}
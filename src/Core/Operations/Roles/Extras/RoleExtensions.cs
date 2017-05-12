using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public static partial class RoleExtensions
    {
        public static RoleRepository RoleRepository(this AppContext context)
        {
            return new RoleRepository(context);
        }

        public static RoleMessage ToRoleMessage(this Role model)
        {
            var message = toRoleMessage(model, new RoleMessage());
            return message;
        }

        public static Role UpdateFrom(this Role model, RoleMessage message)
        {
            return updateFromRoleMessage(message, model);
        }
    }
}
using Core;
using Core.Models.Identity;
using Core.Context;
using Core.Operations.Roles.Extras;
namespace Core.Models.Mappings
{
    public static partial class RoleExtensions
    {
        public static RoleRepository RoleRepository(this DatabaseContext context)
        {
            return new RoleRepository(context);
        }
        public static RoleRow ToRoleRow(this Role model)
        {
            var message = toRoleRow(model, new RoleRow());
            return message;
        }
        public static Role UpdateFrom(this  Role model, RoleRow message)
        {
            return updateFromRoleRow(message, model);
        }
        public static RoleDetail ToRoleDetail(this Role model)
        {
            var message = toRoleDetail(model, new RoleDetail());
            return message;
        }
        public static Role UpdateFrom(this  Role model, RoleDetail message)
        {
            return updateFromRoleDetail(message, model);
        }
    }
}


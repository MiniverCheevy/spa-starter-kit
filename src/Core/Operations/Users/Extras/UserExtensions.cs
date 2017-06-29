using Core;
using Core.Models.Identity;
using Core.Context;
using Core.Identity;
namespace Core.Operations.Users.Extras
{
    public  static partial class UserExtensions
    {
        public static UserRepository UserRepository(this MainContext context)
        {
            return new UserRepository(context);
        }
        
        public static AppPrincipal ToAppPrincipal(this User model)
        {
            var message = toAppPrincipal(model, new AppPrincipal());
            return message;
        }
        public static User UpdateFrom(this  User model, AppPrincipal message)
        {
            return updateFromAppPrincipal(message, model);
            
        }
        public static UserRow ToUserRow(this User model)
        {
            var message = toUserRow(model, new UserRow());
            return message;
        }
        public static User UpdateFrom(this  User model, UserRow message)
        {
            return updateFromUserRow(message, model);
            
        }
        public static UserDetail ToUserDetail(this User model)
        {
            var message = toUserDetail(model, new UserDetail());
            return message;
        }
        public static User UpdateFrom(this  User model, UserDetail message)
        {
            return updateFromUserDetail(message, model);
            
        }
        
    }
}


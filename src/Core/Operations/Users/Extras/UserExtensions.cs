using System.Linq;
using Core.Context;
using Core.Identity;
using Core.Models.Identity;
using Core.Operations.Lists;

namespace Core.Operations.Users.Extras
{
    public static partial class UserExtensions
    {
        public static UserRepository UserRepository(this MainContext context)
        {
            return new UserRepository(context);
        }

        public static UserMessage ToUserMessage(this User model)
        {
            var message = toUserMessage(model, new UserMessage());
            if (model.Roles.Any())
                message.Roles = string.Join(",", model.Roles.Select(x => x.Name));
            return message;
        }

        public static User UpdateFrom(this User model, UserMessage message)
        {
            return updateFromUserMessage(message, model);
        }

        public static UserDetail ToUserDetail(this User model)
        {
            var message = toUserDetail(model, new UserDetail());
            if (model.Roles != null)
                message.Roles = model.Roles.Select(c => new ListItem {Name = c.Name, Value = c.Id}).ToList();
            return message;
        }

        public static User UpdateFrom(this User model, UserDetail message)
        {
            return updateFromUserDetail(message, model);
        }

        public static AppPrincipal ToAppPrincipal(this User model)
        {
            var principal = toAppPrincipal(model, new AppPrincipal());
            principal.UserId = model.Id;
            principal.Roles = model.Roles.Select(c => c.Name).ToList();
            return principal;
        }
    }
}
using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;

namespace Fernweh.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Voodoo;

    public sealed class Configuration : DbMigrationsConfiguration<Fernweh.Core.Context.FernwehContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Fernweh.Core.Context.FernwehContext context)
        {
            addRolesAndUsers(context);
        }

        private static void addRolesAndUsers(FernwehContext context)
        {
            var roles = typeof(Roles).ToINameIdPairList();
            Role admin = null;
            Role user = null;
            foreach (var role in roles)
            {
                var dbRole = new Role {Id = role.Id, Name = role.Name};
                context.Roles.AddOrUpdate(dbRole);
                if (dbRole.Name == Roles.Administrator.ToString())
                    admin = dbRole;
                else if (dbRole.Name == Roles.User.ToString())
                    user = dbRole;
            }

            var shawn = new User {UserName = "Shawn", Roles = new System.Collections.Generic.List<Role> {admin}};
            context.Users.AddOrUpdate(c=> c.UserName, shawn);
        }
    }
}

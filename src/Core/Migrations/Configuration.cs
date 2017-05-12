using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;
using Voodoo;

namespace Fernweh.Core.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MainContext context)
        {
            addRolesAndUsers(context);
        }

        private static void addRolesAndUsers(MainContext context)
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

            var shawn = new User {UserName = "Shawn", Roles = new List<Role> {admin}};
            context.Users.AddOrUpdate(c => c.UserName, shawn);
        }
    }
}
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Core.Context;
using Core.Models.Identity;
using Voodoo;

namespace Core.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            addRolesAndUsers(context);
            addTestData(context);
        }

        private void addTestData(DatabaseContext context)
        {
#if DEBUG
            new TestDataBuilder().Build(context);
#endif
        }

        private static void addRolesAndUsers(DatabaseContext context)
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
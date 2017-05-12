using System.Data.Entity;
using System.Linq;
using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;

namespace Fernweh.Core.Operations.Users.Extras
{
    public class UserRepository
    {
        private MainContext context;

        public UserRepository(MainContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetUserAndRolesQuery()
        {
            var query = context.Users
                .Include(c => c.Roles)
                .AsNoTracking().AsQueryable();

            return query;
        }
    }
}
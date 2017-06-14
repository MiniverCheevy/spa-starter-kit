using System.Data.Entity;
using System.Linq;
using Core.Context;
using Core.Models.Identity;

namespace Core.Operations.Users.Extras
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
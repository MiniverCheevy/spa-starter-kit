using System.Data.Entity;
using System.Linq;
using Fernweh.Core.Context;
using Fernweh.Core.Models.Identity;

namespace Fernweh.Core.Operations.Users.Extras
{
    public class UserRepository
    {
        private FernwehContext context;

        public UserRepository(FernwehContext context)
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
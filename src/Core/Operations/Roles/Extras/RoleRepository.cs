using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public class RoleRepository
    {
        private AppContext context;

        public RoleRepository(AppContext context)
        {
            this.context = context;
        }
    }
}
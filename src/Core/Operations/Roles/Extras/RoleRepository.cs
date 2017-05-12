using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public class RoleRepository
    {
        private MainContext context;

        public RoleRepository(MainContext context)
        {
            this.context = context;
        }
    }
}
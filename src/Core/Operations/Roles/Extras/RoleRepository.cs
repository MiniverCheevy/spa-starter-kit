using Core.Context;

namespace Core.Operations.Roles.Extras
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
using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public class RoleRepository
    {
        private FernwehContext context;

        public RoleRepository(FernwehContext context)
        {
            this.context = context;
        }
    }
}
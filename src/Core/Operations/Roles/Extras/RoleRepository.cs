using Core;
using Core.Models.Identity;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.Roles.Extras
{
    public class RoleRepository
    {
        private DatabaseContext context;
        
        public RoleRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}


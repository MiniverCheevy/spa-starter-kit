using Core;
using Core.Models.Identity;
using Core.Context;
using System.Data.Entity;

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


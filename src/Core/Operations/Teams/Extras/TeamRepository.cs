using Core;
using Core.Models.Scratch;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Operations.Teams.Extras
{
    public class TeamRepository
    {
        private DatabaseContext context;
        
        public TeamRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}


using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

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


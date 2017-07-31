using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.Teams.Extras
{
    public class TeamRepository
    {
        private MainContext context;
        
        public TeamRepository(MainContext context)
        {
            this.context = context;
        }
    }
}


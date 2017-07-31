using Core;
using Core.Models.Scratch;
using Core.Context;
using System.Data.Entity;

namespace Core.Operations.Projects.Extras
{
    public class ProjectRepository
    {
        private MainContext context;
        
        public ProjectRepository(MainContext context)
        {
            this.context = context;
        }
    }
}


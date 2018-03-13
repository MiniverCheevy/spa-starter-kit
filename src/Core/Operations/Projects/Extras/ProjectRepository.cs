using Core;
using Core.Models.Scratch;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Operations.Projects.Extras
{
    public class ProjectRepository
    {
        private DatabaseContext context;
        
        public ProjectRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}

